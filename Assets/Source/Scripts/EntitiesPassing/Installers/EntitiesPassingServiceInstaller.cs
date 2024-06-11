using VContainer;
using VContainer.Unity;

namespace EntitiesPassing
{
    public class EntitiesPassingServiceInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EntitiesPassingService>();
        }
    }
}