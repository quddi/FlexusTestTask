using System;
using ProjectileGeneration;
using ProjectileLifetime;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace MonoPool
{
    public class Projectile : MonoPoolable
    {
        [SerializeField, TabGroup("Components")] private MeshFilter _meshFilter;
        [SerializeField, TabGroup("Components")] private Transform _transform;
        
        [field: SerializeField, ReadOnly] public Vector3? Velocity { get; set; }
        
        private bool _registered;
        private bool _constructed;
        private IProjectileGenerationService _projectileGenerationService;
        private IProjectileLifetimeService _projectileLifetimeService;
        
        public int CollisionsCount { get; private set; }
        
        public float? SpawnTime { get; private set; }

        public Transform Transform => _transform;

        public event Action<Projectile, Collision> OnCollided;

        [Inject]
        private void Construct(IProjectileGenerationService projectileGenerationService, 
            IProjectileLifetimeService projectileLifetimeService)
        {
            _projectileLifetimeService = projectileLifetimeService;
            _projectileGenerationService = projectileGenerationService;

            _constructed = true;
            
            _meshFilter.mesh = _projectileGenerationService.GenerateRandom();
            SpawnTime = Time.time;
            Register();
        }

        public override void ResetState()
        {
            CollisionsCount = 0;
            SpawnTime = null;
            Velocity = null;
        }

        private void OnCollisionEnter(Collision other)
        {
            CollisionsCount++;
            OnCollided?.Invoke(this, other);
        }

        private void Register()
        {
            if (_registered || !_constructed) return;

            _registered = true;
            
            _projectileLifetimeService.Register(this);
        }

        private void UnRegister()
        {
            if (!_registered) return;

            _registered = false;
            
            _projectileLifetimeService.UnRegister(this);
        }

        private void OnEnable()
        {
            if (_projectileGenerationService == null) return;
            
            _meshFilter.mesh = _projectileGenerationService.GenerateRandom();
            SpawnTime = Time.time;
            Register();
        }

        private void OnDisable()
        {
            UnRegister();
        }
    }
}