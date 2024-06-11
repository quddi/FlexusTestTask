using System;
using System.Collections.Generic;
using Camera;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace UI
{
    public class WindowsContainers : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Canvas _canvas;

        [SerializeField, TabGroup("Parameters")] private float _planeDistance;
        
        private ICameraService _cameraService;
        private readonly List<WindowsContainer> _windowsContainers = new();

        public WindowsContainer this[int layer] => _windowsContainers[layer];

        [Inject]
        private void Construct(IUiService uiService, ICameraService cameraService)
        {
            uiService.RegisterWindowsContainers(this);
            
            _cameraService = cameraService;

            OnCameraChangedHandler();
            
            _cameraService.OnCameraChanged += OnCameraChangedHandler;
        }

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                var windowContainer = child.GetComponent<WindowsContainer>();

                if (windowContainer == null)
                    throw new InvalidOperationException($"Found a child without windows container component: [{child}]!");
                
                _windowsContainers.Add(windowContainer);
            }
        }

        public bool ContainsLayer(int layer)
        {
            return layer >= 0 && _windowsContainers.Count > layer;
        }

        private void OnCameraChangedHandler()
        {
            _canvas.worldCamera = _cameraService.Camera;
            _canvas.planeDistance = _planeDistance;
        }

        private void OnDestroy()
        {
            if (_cameraService != null) _cameraService.OnCameraChanged -= OnCameraChangedHandler;
        }
    }
}