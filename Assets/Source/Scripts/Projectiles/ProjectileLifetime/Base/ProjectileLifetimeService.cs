using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Extensions;
using MonoPool;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectileLifetime
{
    public class ProjectileLifetimeService : IProjectileLifetimeService, ITickable
    {
        private ProjectileLifetimeServiceConfig _config;
        
        private IMonoPoolService _monoPoolService;
        private HashSet<Projectile> _activeProjectiles = new();
        private HashSet<Projectile> _explodedProjectiles = new();

        private float DeltaTime => Time.deltaTime * _config.TimeMultiplier;

        [Inject]
        private void Construct(ProjectileLifetimeServiceConfig projectileLifetimeServiceConfig, IMonoPoolService monoPoolService)
        {
            _monoPoolService = monoPoolService;
            _config = projectileLifetimeServiceConfig;
        }
        
        public void Tick()
        {
            foreach (var activeProjectile in _activeProjectiles)
            {
                HandlePosition(activeProjectile);

                if (MustExplode(activeProjectile)) _explodedProjectiles.Add(activeProjectile);
            }

            _activeProjectiles.RemoveRange(_explodedProjectiles);

            while (_explodedProjectiles.Any())
            {
                var projectile = _explodedProjectiles.SnatchFirst();
                
                Explode(projectile).Forget();
            }
        }

        private bool MustExplode(Projectile projectile)
        {
            return projectile.Transform.position.y < _config.MinProjectilePositionY ||
                              projectile.CollisionsCount > _config.MaxCollisionsCount;
        }

        private async UniTaskVoid Explode(Projectile projectile)
        {
            var position = projectile.Transform.position;
            _monoPoolService.Release<Projectile>(projectile);

            var explosionParticles = (ExplosionProjectiles)_monoPoolService.Get<ExplosionProjectiles>(position);

            await explosionParticles.Particles.TryPlayOnce();
            
            _monoPoolService.Release<ExplosionProjectiles>(explosionParticles);
        }
        
        private void HandlePosition(Projectile projectile)
        {
            var nextPosition = GetNextPosition(projectile.Transform.position, projectile.Velocity!.Value);
            var nextVelocity = GetNextVelocity(projectile.Velocity!.Value);

            projectile.Transform.position = nextPosition;
            projectile.Velocity = nextVelocity;
        }

        private Vector3 GetNextPosition(Vector3 currentPosition, Vector3 velocity)
        {
            return new Vector3
            {
                x = currentPosition.x + velocity.x * DeltaTime,
                y = currentPosition.y + velocity.y * DeltaTime,
                z = currentPosition.z + velocity.z * DeltaTime,
            };
        }

        private Vector3 GetNextVelocity(Vector3 currentVelocity)
        {
            return currentVelocity.WithY(currentVelocity.y - Constants.GravityAccelerationValue * DeltaTime);
        }

        public void Register(Projectile projectile)
        {
            _activeProjectiles.Add(projectile);
            projectile.OnCollided += OnProjectileCollidedHandler;
        }

        public void UnRegister(Projectile projectile)
        {
            _activeProjectiles.Remove(projectile);
            projectile.OnCollided -= OnProjectileCollidedHandler;
        }

        private void OnProjectileCollidedHandler(Projectile projectile, Collision collision)
        {
            var contact = collision.GetContact(0);
            var normal = contact.normal;
            
            projectile.Velocity = Vector3.Reflect(projectile.Velocity!.Value, normal) * 
                                  _config.CollisionVelocityDecrease;

            if (contact.otherCollider.gameObject.layer.IsInLMask(_config.DecalLayerMask))
            {
                var bulletMark = (BulletMark)_monoPoolService.Get<BulletMark>();

                bulletMark.ProjectorTransform.position = contact.point + normal / 2;
                bulletMark.ProjectorTransform.LootInDirection(-normal);
            }

            if (MustExplode(projectile))
            {
                _activeProjectiles.Remove(projectile);
                
                Explode(projectile).Forget();
            }
        }
    }
}