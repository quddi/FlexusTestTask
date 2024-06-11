using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public abstract class CustomScope : LifetimeScope
    {
        [SerializeField] private InstallersContainer _installersContainer;
        
        protected override void Configure(IContainerBuilder builder)
        {
            if (_installersContainer == null) 
                return;
            
            _installersContainer.ServicesInstallers.ForEach(service => service.Install(builder));
        }
    }
}