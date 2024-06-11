using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectileLifetime
{
    [CreateAssetMenu(fileName = nameof(ProjectileLifetimeServiceConfig), menuName = "Configs/Projectiles/Projectile lifetime service config")]
    public class ProjectileLifetimeServiceConfig : SerializedScriptableObject
    {
        [field: SerializeField] public float MinProjectilePositionY { get; private set; }
        
        [field: SerializeField] public float TimeMultiplier { get; private set; }
        
        [field: SerializeField] public float CollisionVelocityDecrease { get; private set; }
        
        [field: SerializeField] public int MaxCollisionsCount { get; private set; }
        
        [field: SerializeField] public LayerMask DecalLayerMask { get; private set; }
    }
}