using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.WindowManager
{
	public abstract class BaseWindow : MonoBehaviour
	{
		[FormerlySerializedAs("_name")] [SerializeField]
		protected WindowId id;

		protected bool _isFocused = false;

		public WindowId Id => id;

		protected virtual void OnShow()
		{
		}

		protected virtual void OnHide()
		{
		}

		protected virtual void OnFocus()
		{
		}

		protected virtual void OnLostFocus()
		{
		}

		protected virtual void ProcessInput()
		{
		}

		public void Show()
		{
			gameObject.SetActive(true);
			OnShow();
		}

		public void Hide()
		{
			OnHide();
			gameObject.SetActive(false);
		}

		public void SetFocus()
		{
			if (!_isFocused)
			{
				_isFocused = true;
				OnFocus();
			}
		}

		public void RemoveFocus()
		{
			if (_isFocused)
			{
				_isFocused = false;
				OnLostFocus();
			}
		}
	}
}