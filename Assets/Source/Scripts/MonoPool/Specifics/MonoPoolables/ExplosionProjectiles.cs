using Game;
using UnityEngine;

namespace MonoPool
{
    public class ExplosionProjectiles : MonoPoolable
    {
        [field: SerializeField] public AwaitableParticleSystem Particles { get; private set; } 
        
        public override void ResetState()
        {
            Particles.Stop();
        }
    }
}