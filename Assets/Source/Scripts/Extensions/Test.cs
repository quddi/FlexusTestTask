using Cysharp.Threading.Tasks;
using Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Extensions
{
    public class Test : SerializedMonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private CameraShakeEffect _cameraShakeEffect;
        [SerializeField, TabGroup("Components")] private GunRecoilEffect _gunRecoilEffect;
        
        [Button]
        private void ActCamera()
        {
            _cameraShakeEffect.Execute().Forget();
        }
        
        [Button]
        private void ActGun()
        {
            _gunRecoilEffect.Execute().Forget();
        }

        [Button]
        private void ActTogether()
        {
            _cameraShakeEffect.Execute().Forget();
            _gunRecoilEffect.Execute().Forget();
        }
    }
}