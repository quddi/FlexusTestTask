using System;
using EntitiesPassing;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace UI
{
    public class InputWindow : Window, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, 
        IPointerExitHandler, IInputZone
    {
        [SerializeField, ReadOnly, TabGroup("Parameters")] private bool _down;
        [SerializeField, ReadOnly, TabGroup("Parameters")] private bool _inZone;
        
        private IEntitiesPassingService _entitiesPassingService;

        public bool IsInputActive => _down && _inZone;
        public event Action OnPointerReleased;

        [Inject]
        private void Construct(IEntitiesPassingService entitiesPassingService)
        {
            _entitiesPassingService = entitiesPassingService;
            
            _entitiesPassingService.Set(nameof(IInputZone), this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _down = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _down = false;
            
            if (_inZone) OnPointerReleased?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _inZone = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inZone = false;
        }

        private void OnDestroy()
        {
            _entitiesPassingService?.Remove(nameof(IInputZone));
        }
    }
}