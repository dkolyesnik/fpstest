using Framework.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Components
{
    public abstract class VariableSlider<TValue, TVariable, TVariableId, TVariableRef> : MonoBehaviour
        where TVariable : Variable<TValue>
        where TVariableId : VariableId<TVariable>
        where TVariableRef : VariableRef<TValue, TVariableId, TVariable>

    {
        [Tooltip("Well search slider on wake if empty")]
        [SerializeField]
        protected Slider _slider;
        [SerializeField]
        protected TVariableRef _variable;
        // Start is called before the first frame update
        void Awake()
        {
            if (_slider == null)
            {
                _slider = GetComponent<Slider>();
                if (_slider == null)
                {
                    Debug.LogWarning("Slider is not specified for a slider component watcher. Variable type is " + typeof(TVariable).FullName);
                }
            }
        }

        private void OnEnable()
        {
            _variable.Changed += OnVariableValueChanged;
            OnVariableValueChanged(_variable.GetValue());
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            _variable.Changed -= OnVariableValueChanged;
            if (_slider != null)
                _slider.onValueChanged.RemoveListener(OnSliderValueChanged);

        }

        private void OnVariableValueChanged(TValue value)
        {
            _slider.value = ConvertVariableValueToFloat(value);
        }

        private void OnSliderValueChanged(float sliderValue)
        {
            _variable.SetValue(ConvertFloatToVariableValue(sliderValue));
        }

        protected abstract float ConvertVariableValueToFloat(TValue value);
        protected abstract TValue ConvertFloatToVariableValue(float value);

    }
}
