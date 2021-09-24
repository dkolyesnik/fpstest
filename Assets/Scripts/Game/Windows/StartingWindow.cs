using Framework.WindowManager;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Windows
{
    public class StartingWindow : BaseWindow
    {
        public Button StartButton;
        public Button OptionsButton;
        public Button QuitButton;

        void Start()
        {
            QuitButton.onClick.AddListener(QuitButtonClicked);
        }

        private void QuitButtonClicked()
        {
            if (_isFocused)
                Application.Quit();
        }
    }
}
