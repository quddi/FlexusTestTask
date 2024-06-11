using VContainer;
using VContainer.Unity;

namespace Camera
{
    public class CameraServiceInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CameraService>();
        }
    }
}