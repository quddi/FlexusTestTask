using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace MonoPool
{
    public class BulletMark : MonoPoolable
    {
        [field: SerializeField, TabGroup("Components")] public Transform ProjectorTransform { get; private set; }

        [SerializeField, TabGroup("Parameters")] private TimeSpan _releasingDelay;
        
        private IMonoPoolService _monoPoolService;
        
        [Inject]
        private void Construct(IMonoPoolService monoPoolService)
        {
            _monoPoolService = monoPoolService;
        }
        
        public override void ResetState() { }

        private void OnEnable()
        {
            UniTask.Delay(_releasingDelay)
                .ContinueWith(() => _monoPoolService.Release<BulletMark>(this))
                .Forget();
        }
    }
}