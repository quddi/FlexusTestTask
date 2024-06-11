namespace Game
{
    public class CameraShakeEffect : PositioningVisualEffect
    {
        public void SetCamera(UnityEngine.Camera camera)
        {
            _animatedTransform = camera.transform;
        }
    }
}