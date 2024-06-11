using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UI
{
    public class UiServiceInstaller : IInstaller
    {
        [SerializeField] private UiServiceConfig _uiServiceConfig;
        
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<UiService>()
                .WithParameter("uiServiceConfig", _uiServiceConfig);
        }
    }
}