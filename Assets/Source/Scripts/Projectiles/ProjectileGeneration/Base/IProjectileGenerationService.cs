using UnityEngine;

namespace ProjectileGeneration
{
    public interface IProjectileGenerationService
    {
        Mesh GenerateRandom();
    }
}