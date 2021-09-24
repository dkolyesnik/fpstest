using Framework.Core.VariableTypes.Object;
using Framework.Pool;
using UnityEngine;

namespace Game.SOActions
{
    [CreateAssetMenu(fileName = "Action", menuName = "ScriptableObjects/Actions/ReturnToPool")]
    public class ReturnToPoolSOAction : SOAction
    {
        [SerializeField]
        private ObjectVariableRef _gameObjectPoolRef;
        private GameObjectPool _pool => _gameObjectPoolRef.GetValue() as GameObjectPool;

        public override void Apply(GameObject go)
        {
            _pool.Destroy(go);
        }
    }
}
