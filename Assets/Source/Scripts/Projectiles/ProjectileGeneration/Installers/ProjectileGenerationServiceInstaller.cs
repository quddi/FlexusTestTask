using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectileGeneration
{
    public class ProjectileGenerationServiceInstaller : IInstaller
    {
        [SerializeField] private ProjectileGenerationServiceConfig _projectileGenerationServiceConfig;
        
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ProjectileGenerationService>()
                .WithParameter("projectileGenerationServiceConfig", _projectileGenerationServiceConfig);
        }
    }
}