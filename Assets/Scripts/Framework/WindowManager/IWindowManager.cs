namespace Framework.WindowManager
{
	public interface IWindowManager
	{
		void SwapWindow(WindowId id);
		void Show(WindowId id);
		void Hide(WindowId id);
		void HideAll();
	}
}