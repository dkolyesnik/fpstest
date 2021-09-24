using Framework.Core;
using TMPro;
using UnityEngine;

namespace Framework.Components
{
    public abstract class VariableWatcher<TValue, TVariable, TVariableId, TVariableRef> : MonoBehaviour
        where TVariable : Variable<TValue>
        where TVariableId : VariableId<TVariable>
        where TVariableRef : VariableRef<TValue, TVariableId, TVariable>

    {
        [Tooltip("Will search for TMProText on awake")]
        [SerializeField]
        protected TextMeshProUGUI _text;
        [SerializeField]
        protected TVariableRef _variable;
        // Start is called before the first frame update
        void Awake()
        {
            if (_text == null)
            {
                _text = GetComponent<TextMeshProUGUI>();
                if (_text == null)
                {
                    Debug.LogWarning("Text is not specified for a component watcher. Variable type is " + typeof(TVariable).FullName);
                }
            }

        }

        private void OnValueChanged(TValue value)
        {
            _text.text = value.ToString();
        }

        private void OnEnable()
        {
            _variable.Changed += OnValueChanged;
            OnValueChanged(_variable.GetValue());
        }

        private void OnDisable()
        {
            _variable.Changed -= OnValueChanged;
        }

    }
}
