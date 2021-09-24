using UnityEngine;

namespace Game
{
    public abstract class SOAction : ScriptableObject
    {
        public abstract void Apply(GameObject go);
    }
}
