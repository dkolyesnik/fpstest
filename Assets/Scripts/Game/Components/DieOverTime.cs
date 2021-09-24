using Framework.Core.VariableTypes.Object;
using Framework.Pool;
using UnityEngine;

namespace Game.Components
{
    public class DieOverTime : MonoBehaviour
    {
        [SerializeField]
        private ObjectVariableRef _poolRef;
        private GameObjectPool _pool => _poolRef.GetValue() as GameObjectPool;
        public float Delay = 1f;
        private float _delay = 0f;

        // Update is called once per frame
        void Update()
        {
            _delay += Time.deltaTime;
            if (_delay > Delay)
            {
                _pool.Destroy(gameObject);
                _delay = 0f;
            }

        }
    }
}
