using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.WindowManager
{
	[CreateAssetMenu]
	public class WindowManager : ScriptableObject, IWindowManager
	{
		//TODO support existing canvas
		[SerializeField] private Canvas _canvasPrefab;
		[SerializeField] private List<BaseWindow> _windowPrefabs;

		[NonSerialized]
		private Dictionary<WindowId, BaseWindow> _windowsByName = new Dictionary<WindowId, BaseWindow>();

		[NonSerialized] private List<BaseWindow> _shownWindows = new List<BaseWindow>();
		[NonSerialized] private bool _isInitialized = false;
		[NonSerialized] private Canvas _canvasInstance;

		public void Register(WindowId id, BaseWindow window)
		{
			Initialize();
			if (_windowsByName.ContainsKey(id))
			{
				if (_windowsByName[id] != window)
				{
					Debug.LogWarning("Another window uses the same name");
				}
			}
			else
			{
				_windowsByName.Add(id, window);
			}

			window.Hide();
		}

		private void Initialize()
		{
			if (!_isInitialized)
			{
				_isInitialized = true;
				if (_canvasPrefab == null)
				{
					throw new Exception("Canvas prefab is not specified in WindowManager");
				}

				_canvasInstance = GameObject.Instantiate(_canvasPrefab);

				_windowsByName = new Dictionary<WindowId, BaseWindow>();
				foreach (var windowPrefab in _windowPrefabs)
				{
					if (windowPrefab == null)
					{
						Debug.Log($"Prefab is null in WidowsManager name={name}");
					}
					else
					{
						var windowInstace = GameObject.Instantiate(windowPrefab, _canvasInstance.transform);
						Register(windowPrefab.Id, windowInstace);
					}
				}
			}
		}

		public void SwapWindow(WindowId id)
		{
			if (id == null)
			{
				Debug.LogWarning("Window name is null");
				return;
			}

			Initialize();
			HideAll();
			Show(id);
		}

		public void Show(WindowId id)
		{
			if (id == null)
			{
				Debug.LogWarning("Window name is null");
				return;
			}

			Initialize();
			if (_windowsByName.TryGetValue(id, out var window))
			{
				if (_shownWindows.LastOrDefault() == window)
				{
					return;
				}

				_shownWindows.LastOrDefault()?.RemoveFocus();

				if (!_shownWindows.Remove(window))
				{
					window.Show();
				}

				_shownWindows.Add(window);
				window.SetFocus();
			}
		}

		public void Hide(WindowId id)
		{
			if (id == null)
			{
				Debug.LogWarning("Window name is null");
				return;
			}

			Initialize();
			if (_windowsByName.TryGetValue(id, out var window) && _shownWindows.Remove(window))
			{
				window.RemoveFocus();
				window.Hide();
				_shownWindows.LastOrDefault()?.SetFocus();
			}
		}

		public void HideAll()
		{
			Initialize();
			foreach (var window in _shownWindows)
			{
				window.RemoveFocus();
				window.Hide();
			}

			_shownWindows.Clear();
		}
	}
}