using UnityEngine;

namespace Game.PlayerData
{
    public abstract class PlayerData : ScriptableObject
    {
        public abstract void SetFloat(string name, float value);
        public abstract float GetFloat(string name, float defaultValue);

        public abstract void Save();
        public abstract void Load();
    }
}
