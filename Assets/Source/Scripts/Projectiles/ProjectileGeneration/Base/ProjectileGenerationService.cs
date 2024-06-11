using Extensions;
using UnityEngine;
using VContainer;

namespace ProjectileGeneration
{
    public class ProjectileGenerationService : IProjectileGenerationService
    {
        private ProjectileGenerationServiceConfig _config;

        [Inject]
        private void Construct(ProjectileGenerationServiceConfig projectileGenerationServiceConfig)
        {
            _config = projectileGenerationServiceConfig;
        }
        
        public Mesh GenerateRandom()
        {
            return _config.MeshGenerators.Random().GetNext();
        }
    }
}