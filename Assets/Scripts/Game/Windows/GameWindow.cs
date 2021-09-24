using Framework.Core.VariableTypes.Bool;
using Framework.Core.VariableTypes.Int;
using Framework.Core.VariableTypes.Object;
using Framework.Pool;
using Framework.WindowManager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Windows
{
    public class GameWindow : BaseWindow
    {
        [FormerlySerializedAs("_settingsWindowName")] [SerializeField]
        private WindowId settingsWindowId;

        [SerializeField]
        private BoolVariableRef _pauseEvent;

        [SerializeField]
        private ObjectVariableRef _windowManagerRef;
        private WindowManager _windowManager => _windowManagerRef.GetValue() as WindowManager;

        [SerializeField]
        private ObjectVariableRef _poolRef;
        private GameObjectPool _pool => _poolRef.GetValue() as GameObjectPool;

        [SerializeField]
        private ObjectVariableRef _gameSettingsRef;
        private GameSettings _gameSettings => _gameSettingsRef.GetValue() as GameSettings;

        [SerializeField]
        private IntVariableRef _scores;

        private bool _isSceneLoaded = false;

        private GameObject _player;
        private GameObject _level;
        private bool _isPaused;


        override protected void OnShow()
        {
            var currentLevel = _gameSettings.GetCurrentLevel();
            _level = _pool.Create(currentLevel.Level);
            var spawnPointScript = _level.GetComponent<PlayerSwanPoint>();
            if (spawnPointScript == null)
            {
                _player = _pool.Create(currentLevel.Player);
            }
            else
            {
                _player = _pool.Create(currentLevel.Player, spawnPointScript.SpawnPoint.transform.position, spawnPointScript.SpawnPoint.transform.rotation);
            }

            _scores.SetValue(0);

        }

        private void SetPause(bool pause)
        {
            _isPaused = pause;
            // _level.SetPause(pause);
            // _player.SetPause(pause);
            if (_isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            _pauseEvent.SetValue(_isPaused);
        }

        override protected void OnFocus()
        {
            SetPause(false);
        }

        override protected void OnLostFocus()
        {
            SetPause(true);
        }


        override protected void OnHide()
        {
            if (_player != null)
            {
                _pool.Destroy(_player.gameObject);
            }
            if (_level != null)
            {
                _pool.Destroy(_level.gameObject);
            }

        }


        private void Update()
        {
            if (_isFocused && Input.GetButtonDown(InputConstants.CANCEL))
            {
                _windowManager.Show(settingsWindowId);
            }

        }
    }
}
