using UnityEngine;
using VContainer;

namespace Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraSetter : MonoBehaviour
    {
        [Inject]
        private void Construct(ICameraService cameraService)
        {
            cameraService.SetCamera(GetComponent<UnityEngine.Camera>());
        }
    }
}