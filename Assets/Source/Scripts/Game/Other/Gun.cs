using Cysharp.Threading.Tasks;
using EntitiesPassing;
using MonoPool;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using VContainer;

namespace Game
{
    public class Gun : SerializedMonoBehaviour
    {
        [SerializeField, TabGroup("Parameters")] private float _minPower;
        [SerializeField, TabGroup("Parameters")] private float _maxPower;
        [SerializeField, TabGroup("Parameters")] private IVisualEffect _shotEffect;

        [SerializeField, TabGroup("Components")] private Transform _backPoint;
        [SerializeField, TabGroup("Components")] private Transform _frontPoint;
        [SerializeField, TabGroup("Components")] private PlayerRotation _playerRotation;
        [SerializeField, TabGroup("Components")] private TrajectoryDrawer _trajectoryDrawer;

        private IInputZone _inputZone;
        private IFirePowerProvider _firePowerProvider;
        private IMonoPoolService _monoPoolService;
        private IEntitiesPassingService _entitiesPassingService;

        private Vector3 Direction => (_frontPoint.position - _backPoint.position).normalized;
        private float Power => Mathf.Lerp(_minPower, _maxPower, _firePowerProvider.RelativePower);

        private Vector3 StartVelocity => Direction * Power;

        [Inject]
        private void Construct(IEntitiesPassingService entitiesPassingService, IMonoPoolService monoPoolService)
        {
            _monoPoolService = monoPoolService;
            _entitiesPassingService = entitiesPassingService;

            OnEntitySetHandler(nameof(IFirePowerProvider), _entitiesPassingService.Get(nameof(IFirePowerProvider)));
            
            _entitiesPassingService.OnEntitySet += OnEntitySetHandler;
            _entitiesPassingService.OnEntityRemoved += OnEntityRemovedHandler;
            
            UniTask.WaitUntil(() => _firePowerProvider != null)
                .ContinueWith(() => _trajectoryDrawer.RefreshTrajectory(_frontPoint.position, StartVelocity))
                .Forget();
        }

        private void Shot()
        {
            if (_shotEffect.IsExecuting) return;
            
            var projectile = (Projectile)_monoPoolService.Get<Projectile>(_frontPoint.position);

            projectile.Velocity = StartVelocity;

            _shotEffect.Execute().Forget();
        }

        private void OnPointerReleasedHandler()
        {
            Shot();
        }

        private void OnEntitySetHandler(string entityKey, object entity)
        {
            if (entity == null) return;

            if (entityKey == nameof(IFirePowerProvider))
            {
                _firePowerProvider = (IFirePowerProvider)entity;
                _firePowerProvider.OnPowerChanged += OnFirePowerChangedHandler;
            }

            if (entityKey == nameof(IInputZone))
            {
                _inputZone = (IInputZone)entity;
                _inputZone.OnPointerReleased += OnPointerReleasedHandler;
            }
        }

        private void OnEntityRemovedHandler(string entityKey, object _)
        {
            if (entityKey == nameof(IFirePowerProvider))
            {
                _firePowerProvider.OnPowerChanged -= OnFirePowerChangedHandler;
                _firePowerProvider = null;
            }
            
            if (entityKey == nameof(IInputZone))
            {
                _inputZone.OnPointerReleased -= OnPointerReleasedHandler;
                _inputZone = null;
            }
        }
        
        private void OnFirePowerChangedHandler(float newPower)
        {
            _trajectoryDrawer.RefreshTrajectory(_frontPoint.position, StartVelocity);
        }

        private void OnPlayerRotationChangedHandler()
        {
            _trajectoryDrawer.RefreshTrajectory(_frontPoint.position, StartVelocity);
        }

        private void OnEnable()
        {
            _playerRotation.OnRotationChanged += OnPlayerRotationChangedHandler;
        }

        private void OnDisable()
        {
            _playerRotation.OnRotationChanged -= OnPlayerRotationChangedHandler;
        }

        private void OnDestroy()
        {
            if (_firePowerProvider != null) _firePowerProvider.OnPowerChanged -= OnFirePowerChangedHandler;
            
            if (_inputZone != null) _inputZone.OnPointerReleased -= OnPointerReleasedHandler;

            if (_entitiesPassingService == null) return;
            
            _entitiesPassingService.OnEntitySet -= OnEntitySetHandler;
            _entitiesPassingService.OnEntityRemoved -= OnEntityRemovedHandler;
        }
    }
}