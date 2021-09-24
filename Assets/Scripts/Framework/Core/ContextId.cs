using UnityEngine;

namespace Framework.Core
{
    [CreateAssetMenu]
    public class ContextId : ScriptableObject
    {
        public Context DefaultContextPrefab => _defaultContextPrefab;
        [SerializeField]
        private Context _defaultContextPrefab;

    }
}
