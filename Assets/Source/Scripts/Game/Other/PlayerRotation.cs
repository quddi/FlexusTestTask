using System;
using EntitiesPassing;
using Extensions;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using VContainer;

namespace Game
{
    public class PlayerRotation : SerializedMonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Transform _player;
        [SerializeField, TabGroup("Components")] private Transform _gun;

        [SerializeField, TabGroup("Parameters")] private Vector2 _sensivity;
        [SerializeField, TabGroup("Parameters"), SuffixLabel("Degrees")] private float _maxPlayerDeltaAngle;
        [SerializeField, TabGroup("Parameters"), SuffixLabel("Degrees")] private float _minGunAngle;
        [SerializeField, TabGroup("Parameters"), SuffixLabel("Degrees")] private float _maxGunAngle;
        
        private Vector3? _previousPosition;
        private IInputZone _inputZone;
        private IEntitiesPassingService _entitiesPassingService;

        private const float MiddleAngle = 90f;

        public event Action OnRotationChanged;

        [Inject]
        private void Construct(IEntitiesPassingService entitiesPassingService)
        {
            _entitiesPassingService = entitiesPassingService;
            
            _entitiesPassingService.OnEntitySet += OnEntitySetHandler;
            _entitiesPassingService.OnEntityRemoved += OnEntityRemovedHandler;
        }

        private void Start()
        {
            _player.localRotation = Quaternion.Euler(Vector3.zero.WithY(MiddleAngle));
        }

        private void Update()
        {
            if (!Input.GetMouseButton(0) || _inputZone is not { IsInputActive: true })
            {
                _previousPosition = null;
                return;
            }

            if (_previousPosition == null)
            {
                _previousPosition = Input.mousePosition;
                return;
            }
            
            var delta = Input.mousePosition - _previousPosition!.Value;
                
            _previousPosition = Input.mousePosition;

            HandleX(delta);
            HandleY(delta);
            
            OnRotationChanged?.Invoke();
        }

        private void HandleX(Vector3 delta)
        {
            var eulerAngles = _player.localRotation.eulerAngles;

            var newY = Mathf.Clamp
            (
                eulerAngles.y + delta.x * _sensivity.x, 
                MiddleAngle - _maxPlayerDeltaAngle, 
                MiddleAngle + _maxPlayerDeltaAngle
            );

            _player.localRotation = Quaternion.Euler(eulerAngles.WithY(newY));
        }

        private void HandleY(Vector3 delta)
        {
            var eulerAngles = _gun.localRotation.eulerAngles;

            var newX = Mathf.Clamp(eulerAngles.x - delta.y * _sensivity.y, _minGunAngle, _maxGunAngle);

            _gun.localRotation = Quaternion.Euler(eulerAngles.WithX(newX));
        }

        private void OnEntitySetHandler(string key, object entity)
        {
            if (key == nameof(IInputZone))
            {
                _inputZone = (IInputZone)entity;
            }
        }

        private void OnEntityRemovedHandler(string key, object entity)
        {
            if (key == nameof(IInputZone))
            {
                _inputZone = null;
            }
        }

        private void OnDestroy()
        {
            if (_entitiesPassingService == null) return;
            
            _entitiesPassingService.OnEntitySet -= OnEntitySetHandler;
            _entitiesPassingService.OnEntityRemoved -= OnEntityRemovedHandler;        
        }
    }
}