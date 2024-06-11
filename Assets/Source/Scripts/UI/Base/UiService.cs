using System;
using System.Collections.Generic;
using Extensions;
using VContainer;
using VContainer.Unity;

namespace UI
{
    public class UiService : IUiService
    {
        private UiServiceConfig _uiServiceConfig;
        
        private IObjectResolver _objectResolver;
        private WindowsContainers _windowsContainers;
        private Dictionary<Type, (Window window, int layer)> _activeWindows = new();
        private Dictionary<Type, Window> _inactiveWindows = new();

        [Inject]
        private void Construct(UiServiceConfig uiServiceConfig, IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            _uiServiceConfig = uiServiceConfig;
        }
        
        public T TryShowWindow<T>(int layer) where T : Window
        {
            if (!_windowsContainers.ContainsLayer(layer))
                throw new ArgumentException($"Unknown layer: {layer}");
            
            var key = typeof(T);

            if (_activeWindows.TryGetValue(key, out (Window window, int layer) value))
            {
                if (value.layer != layer)
                {
                    value.window.transform.parent = _windowsContainers[layer].Transform;
                    _activeWindows[key] = (value.window, layer);
                }
                
                return (T)value.window;
            }

            var window = _inactiveWindows.ContainsKey(key)
                ? _inactiveWindows.Snatch(key)
                : CreateWindow(key, _windowsContainers[layer]);
            
            window.gameObject.SetActive(true);
            window.transform.parent = _windowsContainers[layer].Transform;
            
            _activeWindows[key] = (window, layer);

            return (T)window;
        }

        public bool TryHideWindow<T>() where T : Window
        {
            if (!IsWindowActive<T>())
                return false;
            
            var key = typeof(T);

            var window = _activeWindows.Snatch(key).window;
            
            window.gameObject.SetActive(false);

            window.transform.parent = null;
            
            _inactiveWindows[key] = window;

            return true;
        }

        public bool IsWindowActive<T>() where T : Window
        {
            return _activeWindows.ContainsKey(typeof(T));
        }

        public void RegisterWindowsContainers(WindowsContainers windowsContainers)
        {
            _windowsContainers = windowsContainers;
        }
        
        private Window CreateWindow(Type type, WindowsContainer windowsContainer)
        {
            if (!_uiServiceConfig.WindowsPrefabs.ContainsKey(type))
                throw new ArgumentException($"Prefab for window of type [{type}] not found!");

            var prefab = _uiServiceConfig.WindowsPrefabs[type];

            var window = _objectResolver.Instantiate(prefab, windowsContainer.Transform);

            return window;
        }
    }
}