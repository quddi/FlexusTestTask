using System;

namespace Camera
{
    public interface ICameraService
    {
        public UnityEngine.Camera Camera { get; }

        public event Action OnCameraChanged;

        public void SetCamera(UnityEngine.Camera camera);
    }
}