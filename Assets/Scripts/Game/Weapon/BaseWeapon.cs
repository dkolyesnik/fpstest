using Framework.Pool;
using UnityEngine;

namespace Game.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour, IPausable, IResatable
    {

        public enum WeaponState
        {
            Shown,
            Hidden
        }
        [SerializeField]
        protected float _shootingDelay;
        protected float _timeFromTheLastShot;

        [SerializeField]
        protected AudioClip _shotSound;
        [SerializeField]
        protected AudioSource _audioSource;

        [SerializeField]
        protected float _impactForce;
        [SerializeField]
        protected int _damage;
        [SerializeField]
        protected DamageType _damageType;
        [SerializeField]
        protected Transform _targetingObject;
        [SerializeField]
        protected bool _autoFire = false;
        [SerializeField]
        protected BaseImpact _impact;

        protected WeaponState _state = WeaponState.Hidden;
        private bool _isPaused = false;

        private void Start()
        {
            if (_impact == null)
            {
                Debug.LogError("Impact is non specified");
            }
        }


        void Update()
        {
            if (_isPaused)
            {
                return;
            }
            var deltaTime = Time.deltaTime;
            _timeFromTheLastShot += deltaTime;

            if (CanShoot() && NeedToShoot())
            {
                _timeFromTheLastShot = 0f;
                Shoot();
            }
        }

        public void SetPause(bool pause)
        {
            _isPaused = pause;
        }

        public void Show(bool fast = false)
        {
            if (_state != WeaponState.Hidden)
            {
                return;
            }

            _state = WeaponState.Shown;
            //TODO: showing
        }

        public void Hide(bool fast = false)
        {
            if (_state != WeaponState.Shown)
            {
                return;
            }

            _state = WeaponState.Hidden;
            //TODO: hiding

        }

        protected virtual bool NeedToShoot()
        {

            if (_autoFire)
            {
                return Input.GetButton(InputConstants.FIRE);

            }
            else
            {
                return Input.GetButtonDown(InputConstants.FIRE);
            }
        }

        protected virtual bool CanShoot()
        {
            return Time.timeScale != 0f && _state == WeaponState.Shown && _timeFromTheLastShot > _shootingDelay;
        }

        protected abstract void Shoot();

        public void Reset()
        {
            _timeFromTheLastShot = 0f;
            //TODO remove all impacts
        }
    }
}
