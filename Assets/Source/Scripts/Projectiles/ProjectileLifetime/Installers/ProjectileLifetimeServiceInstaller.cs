using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectileLifetime
{
    public class ProjectileLifetimeServiceInstaller : IInstaller
    {
        [SerializeField] private ProjectileLifetimeServiceConfig _projectileLifetimeServiceConfig;
        
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ProjectileLifetimeService>()
                .WithParameter("projectileLifetimeServiceConfig", _projectileLifetimeServiceConfig);
        }
    }
}