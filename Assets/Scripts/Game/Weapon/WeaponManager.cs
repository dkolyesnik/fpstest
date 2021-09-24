using System.Collections.Generic;
using Framework.Core.VariableTypes.Bool;
using Framework.Pool;
using UnityEngine;

namespace Game.Weapon
{
    public class WeaponManager : MonoBehaviour, IPausable, IResatable
    {
        [SerializeField]
        private BoolVariableRef _gamePause;
        [SerializeField]
        private List<BaseWeapon> _weapons;
        // Start is called before the first frame update
        private int _selectedWeapon = 0;
        private bool _isPaused;

        void Start()
        {

            if (_weapons.Count == 0)
            {
                Debug.Log("There are no weapons");
            }
            else
            {
                foreach (var weapon in _weapons)
                {
                    weapon.Hide(true);
                }
                _weapons[0].Show(true);
            }
        }

        private void OnEnable()
        {
            _gamePause.Changed += SetPause;
        }
        private void OnDisable()
        {
            _gamePause.Changed -= SetPause;
        }

        public void SetPause(bool pause)
        {
            _isPaused = pause;

            foreach (var weapon in _weapons)
            {
                weapon.SetPause(pause);
            }
        }

        private void HideWeapons()
        {
            foreach (var weapon in _weapons)
            {
                weapon.Hide(true);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_isPaused)
            {
                return;
            }
            int nextWeapon = -1;
            if (Input.GetButtonDown(InputConstants.WEAPON_1))
            {
                Debug.Log(InputConstants.WEAPON_1);
                nextWeapon = 0;

            }
            else if (Input.GetButtonDown(InputConstants.WEAPON_2))
            {
                Debug.Log(InputConstants.WEAPON_2);
                nextWeapon = 1;
            }
            if (nextWeapon != -1 && _selectedWeapon != nextWeapon)
            {
                HideWeapons();
                _selectedWeapon = nextWeapon;
                _weapons[_selectedWeapon].Show();
            }

        }

        public void Reset()
        {
            foreach (var weapon in _weapons)
                weapon.Reset();
        }
    }
}
