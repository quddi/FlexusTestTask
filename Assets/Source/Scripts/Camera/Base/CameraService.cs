using System;

namespace Camera
{
    public class CameraService : ICameraService
    {
        public UnityEngine.Camera Camera { get; private set; }
        
        public event Action OnCameraChanged;
        
        public void SetCamera(UnityEngine.Camera camera)
        {
            Camera = camera;
            
            OnCameraChanged?.Invoke();
        }
    }
}