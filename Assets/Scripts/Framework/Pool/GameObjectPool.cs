using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Pool
{
	[CreateAssetMenu]
	public class GameObjectPool : ScriptableObject
	{
		[Serializable]
		public class PrefabSettings
		{
			public GameObject prefab;
			public bool IsPoolEnabled = true;

			public bool NeedReset = false;
			public PrefabSettings Clone()
			{
			    var newSettings = new PrefabSettings();
			    newSettings.prefab = prefab;
			    newSettings.IsPoolEnabled = IsPoolEnabled;
			    return newSettings;
			}
		}

		[SerializeField] private PrefabSettings _defaultSettings;
		[SerializeField] private List<PrefabSettings> _prefabsSettings;

		[NonSerialized] private Dictionary<int, Pool> _poolsByPrefabId = new Dictionary<int, Pool>();
		[NonSerialized] private GameObject _holder;
		[NonSerialized] private bool _isInitialized = false;

		protected void Awake()
		{
			CheckIfInitialized();
			// Initialize();
		}

		public GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
		{
			CheckIfInitialized();
			if (prefab == null)
			{
				return null;
			}

			var pool = GetOrCreatePool(prefab);
			if (pool == null)
			{
				return GameObject.Instantiate(prefab, position, rotation, parent);
			}
			else
			{
				var obj = pool.GetObject();
				if (obj == null)
				{
					var go = GameObject.Instantiate(prefab, position, rotation, parent);
					pool.RegisterActiveObject(go.GetInstanceID());
					var poolData = go.AddComponent<PoolData>();
					poolData.Initialize(this, prefab);
					return go;
				}
				else
				{
					obj.transform.position = position;
					obj.transform.rotation = rotation;
					obj.transform.SetParent(parent);
					obj.gameObject.SetActive(true);
					return obj;
				}
			}
		}
		//TODO
		// public T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
		// {

		// }
		// public T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
		// {

		// }
		// public T Instantiate<T>(T original) where T : Object
		// {
		// }
		// public T Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object
		// {

		// }
		public GameObject Create(GameObject prefab, Transform parent = null)
		{
			CheckIfInitialized();
			if (prefab == null)
				return null;

			var pool = GetOrCreatePool(prefab);

			if (pool == null)
				return Instantiate(prefab, parent);

			var obj = pool.GetObject();
			if (obj == null)
			{
				var go = Instantiate(prefab, parent);
				pool.RegisterActiveObject(go.GetInstanceID());
				var poolData = go.AddComponent<PoolData>();
				poolData.Initialize(this, prefab);
				return go;
			}

			obj.transform.SetParent(parent);
			obj.gameObject.SetActive(true);
			return obj;
		}

		public void Destroy(GameObject gameObject)
		{
			CheckIfInitialized();
			var poolData = gameObject.GetComponent<PoolData>();
			if (poolData != null)
			{
				var pool = GetOrCreatePool(poolData.Prefab);
				if (pool != null)
				{
					pool.ReturnObject(gameObject);
					return;
				}
			}

			GameObject.Destroy(gameObject);
		}

		public void NotifyDestroyed(GameObject prefab, int instanceId)
		{
			CheckIfInitialized();
			GetPool(prefab)?.NotifyDestroyed(instanceId);
		}

		// need this to fill the dictionary
		 private void CheckIfInitialized()
		 {
		     if (!_isInitialized)
		         Initialize();
		 }
		private void Initialize()
		{
			_isInitialized = true;
			_holder = new GameObject();
			_holder.name = "PoolHolder";

			foreach (var prefabSettings in _prefabsSettings)
			{
				if (prefabSettings.prefab == null)
				{
					Debug.LogError("Prefab should not be null in GameObjectPool's prefab settings");
				}
				else if (_poolsByPrefabId.ContainsKey(prefabSettings.prefab.GetInstanceID()))
				{
					Debug.LogError("Duplicatig prefabs in GameObjectPool's prefab settings");
				}
				else
				{
					_poolsByPrefabId[prefabSettings.prefab.GetInstanceID()] =
						new Pool(prefabSettings, _holder.transform);
				}
			}
		}

		private Pool GetOrCreatePool(GameObject prefab)
		{
			var prefabId = prefab.GetInstanceID();
			Pool pool;
			if (!_poolsByPrefabId.TryGetValue(prefabId, out pool))
			{
				if (_defaultSettings.IsPoolEnabled)
				{
					var newSettings = _defaultSettings.Clone();
					newSettings.prefab = prefab;
					pool = new Pool(_defaultSettings, _holder.transform);
					_poolsByPrefabId[prefabId] = pool;
				}
				else
				{
					return null;
				}
			}

			return pool;
		}

		private Pool GetPool(GameObject prefab)
		{
			if (_poolsByPrefabId.TryGetValue(prefab.GetInstanceID(), out var pool))
			{
				return pool;
			}

			return null;
		}


		internal class PoolData : MonoBehaviour
		{
			public GameObject Prefab => _prefab;
			private GameObject _prefab;
			private GameObjectPool _gameObjectPool;

			internal void Initialize(GameObjectPool gameObjectPool, GameObject prefab)
			{
				_gameObjectPool = gameObjectPool;
				_prefab = prefab;
			}

			private void OnDestroy()
			{
				_gameObjectPool.NotifyDestroyed(_prefab, gameObject.GetInstanceID());
			}
		}

		internal class Pool
		{
			private PrefabSettings _settings;
			private Stack<GameObject> _objects = new Stack<GameObject>();
			private HashSet<int> _activeObjectsIds = new HashSet<int>();
			private Transform _poolHolder;

			public Pool(PrefabSettings settings, Transform poolHolder)
			{
				_settings = settings;
				_poolHolder = poolHolder;
			}

			public GameObject GetObject()
			{
				if (_objects.Count > 0)
					return _objects.Pop();
				else
					return null;
			}

			public void ReturnObject(GameObject gameObject)
			{
				if (_settings.NeedReset)
				{
					foreach (var r in gameObject.GetComponentsInChildren<IResatable>())
					{
						r.Reset();
					}
				}

				gameObject.SetActive(false);
				gameObject.transform.SetParent(_poolHolder);
				_objects.Push(gameObject);
				_activeObjectsIds.Remove(gameObject.GetInstanceID());
			}

			public void RegisterActiveObject(int instanceId)
			{
				_activeObjectsIds.Add(instanceId);
			}

			internal void NotifyDestroyed(int instanceId)
			{
				_activeObjectsIds.Remove(instanceId);
			}
		}
	}
}