using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class AwaitableParticleSystem : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private ParticleSystem _particleSystem;
        
        [SerializeField, TabGroup("Parameters")] private bool _stopOnEnable;
        [SerializeField, TabGroup("Parameters")] private bool _useCustomDuration;
        [SerializeField, TabGroup("Parameters"), ShowIf(nameof(_useCustomDuration))] private float _customDuration;
        
        private bool _isPlaying;
        
        public async UniTask<bool> TryPlayOnce()
        {
            if (_isPlaying) return false;

            _isPlaying = true;
            
            var mainParticleSystem = _particleSystem.main;
            
            var delay = TimeSpan.FromSeconds(_useCustomDuration 
                ? _customDuration 
                : mainParticleSystem.duration);
            
            _particleSystem.Play();

            await UniTask.Delay(delay);

            _isPlaying = false;

            return true;
        }

        public async UniTask<bool> TryPlay(int times)
        {
            for (int i = 0; i < times; i++)
            {
                var playResult = await TryPlayOnce();

                if (playResult == false) return false;
            }

            return true;
        }

        public void Stop()
        {
            _isPlaying = false;
            _particleSystem.Stop();
        }
        
        private void OnEnable()
        {
            if (_stopOnEnable) Stop();
        }
    }
}