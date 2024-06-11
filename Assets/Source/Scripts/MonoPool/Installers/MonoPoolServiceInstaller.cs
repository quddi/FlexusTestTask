using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MonoPool
{
    public class MonoPoolServiceInstaller : IInstaller
    {
        [SerializeField] private MonoPoolServiceConfig _monoPoolServiceConfig;
        
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MonoPoolService>()
                .WithParameter("monoPoolServiceConfig", _monoPoolServiceConfig);
        }
    }
}