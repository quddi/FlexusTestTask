using MonoPool;

namespace ProjectileLifetime
{
    public interface IProjectileLifetimeService
    {
        void Register(Projectile projectile);
        
        void UnRegister(Projectile projectile);
    }
}