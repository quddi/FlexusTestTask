using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectileGeneration
{
    [CreateAssetMenu(fileName = nameof(ProjectileGenerationServiceConfig), menuName = "Configs/Projectiles/Projectile generation service config")]
    public class ProjectileGenerationServiceConfig : SerializedScriptableObject
    {
        [field: SerializeField] public List<IMeshGenerator> MeshGenerators { get; private set; } = new();
    }
}