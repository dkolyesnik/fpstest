using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game.PlayerData
{
    [CreateAssetMenu(fileName = "FilePlayerData", menuName = "ScriptableObjects/PlayerData/FilePlayerData")]
    public class FilePlayerData : PlayerData
    {
        [SerializeField]
        private string _fileName;
        private string _filePath => Application.persistentDataPath + Path.DirectorySeparatorChar + _fileName;

        [NonSerialized]
        private Dictionary<string, float> _floatValues = new Dictionary<string, float>();
        private bool _isDirty = false;


        public override float GetFloat(string name, float defaultValue)
        {
            if (_floatValues.TryGetValue(name, out var value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        public override void Load()
        {

            using (var stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (stream.Length > 0)
                {
                    _floatValues = new BinaryFormatter().Deserialize(stream) as Dictionary<string, float>;
                }
                else
                {
                    _floatValues = new Dictionary<string, float>();
                }
            }
            _isDirty = false;
        }

        public override void Save()
        {
            if (_isDirty)
            {
                using (var stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(stream, _floatValues);
                }
                _isDirty = false;
            }
        }
        public override void SetFloat(string name, float value)
        {
            _floatValues[name] = value;
            _isDirty = true;
        }
    }
}
