using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "DamageType", menuName = "ScriptableObjects/DamageType")]
    public class DamageType : ScriptableObject
    {
        public int Score => _score;
        [SerializeField]
        private int _score;
    }
}
