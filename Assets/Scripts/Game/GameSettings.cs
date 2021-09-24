using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public IReadOnlyList<LevelData> Levels => _levels;
        [SerializeField]
        private List<LevelData> _levels;

        public int SelectedLevel
        {
            get
            {
                return _selectedLevel;
            }
            set
            {
                if (value >= _levels.Count)
                {
                    value = _levels.Count - 1;
                }
                if (value < 0)
                {
                    value = 0;
                }

                _selectedLevel = value;
            }
        }
        [NonSerialized]
        private int _selectedLevel = 0;

        public LevelData GetCurrentLevel()
        {
            return _levels[SelectedLevel];
        }
    }

    [Serializable]
    public class LevelData
    {
        public GameObject Level;
        public GameObject Player;
    }
}