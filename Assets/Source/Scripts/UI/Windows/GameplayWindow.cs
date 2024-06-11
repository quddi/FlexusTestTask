using System;
using EntitiesPassing;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class GameplayWindow : Window, IFirePowerProvider
    {
        [SerializeField, TabGroup("Components")] private Slider _slider;
        [SerializeField, TabGroup("Components")] private TMP_Text _powerText;
        
        private IEntitiesPassingService _entitiesPassingService;

        public float RelativePower => Mathf.InverseLerp(_slider.minValue, _slider.maxValue, _slider.value);
        public event Action<float> OnPowerChanged;

        [Inject]
        private void Construct(IEntitiesPassingService entitiesPassingService)
        {
            _entitiesPassingService = entitiesPassingService;
            
            _entitiesPassingService.Set(nameof(IFirePowerProvider), this);
        }

        private void OnSliderValueChangedHandler(float newValue)
        {
            _powerText.text = Mathf.RoundToInt(_slider.value).ToString();
            OnPowerChanged?.Invoke(newValue);
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChangedHandler);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChangedHandler);
        }

        private void OnDestroy()
        {
            _entitiesPassingService?.Remove(nameof(IFirePowerProvider));
        }
    }
}