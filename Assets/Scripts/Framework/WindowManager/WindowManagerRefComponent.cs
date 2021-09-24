using Framework.Core.VariableTypes.Object;
using UnityEngine;

namespace Framework.WindowManager
{
    public class WindowManagerRefComponent : MonoBehaviour, IWindowManager
    {
        [SerializeField] private ObjectVariableRef _windowManagerRef;
        private IWindowManager _windowManager => _windowManagerRef.GetValue() as IWindowManager;

        public void SwapWindow(WindowId id)
        {
            _windowManager.SwapWindow(id);
        }

        public void Show(WindowId windowId)
        {
            _windowManager.Show(windowId);
        }

        public void Swap(WindowId windowId)
        {
            _windowManager.SwapWindow(windowId);
        }

        public void Hide(WindowId windowId)
        {
            _windowManager.Hide(windowId);
        }

        public void HideAll()
        {
            _windowManager.HideAll();
        }
    }
}