using Framework.Core.VariableTypes.Object;
using Framework.WindowManager;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Windows
{
    public class SettingsWindow : BaseWindow
    {
        [SerializeField]
        private ObjectVariableRef _playerData;
        [SerializeField]
        private Button CloseButton;

        [SerializeField]
        private ObjectVariableRef _windowManagerRef;
        private WindowManager _windowManager => _windowManagerRef.GetValue() as WindowManager;

        [SerializeField]
        private ObjectVariableRef _savableDataRef;
        private SavableData _savableData => _savableDataRef.GetValue() as SavableData;

        void Start()
        {
            CloseButton.onClick.AddListener(Close);
        }

        void Update()
        {
            if (_isFocused && Input.GetButtonDown(InputConstants.CANCEL))
            {
                Close();
            }
        }

        public void Close()
        {

            _savableData.Save();

            _windowManager.Hide(Id);
        }
    }
}
