using UnityEngine;

namespace Game.PlayerData
{
    [CreateAssetMenu(fileName = "PlayerPrefsPlayerData", menuName = "ScriptableObjects/PlayerData/PlayerPrefsPlayerData")]
    public class PlayerPrefsPlayerData : PlayerData
    {
        public override float GetFloat(string name, float defaultValue)
        {
            return PlayerPrefs.GetFloat(name, defaultValue);
        }

        public override void Load()
        {
        }

        public override void Save()
        {
            PlayerPrefs.Save();
        }

        public override void SetFloat(string name, float value)
        {
            PlayerPrefs.SetFloat(name, value);
        }

    }
}
