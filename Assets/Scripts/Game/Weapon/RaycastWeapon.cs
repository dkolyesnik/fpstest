using UnityEngine;

namespace Game.Weapon
{
    public class RaycastWeapon : BaseWeapon
    {
        protected override void Shoot()
        {
            if (_audioSource != null && _shotSound != null)
            {
                _audioSource.PlayOneShot(_shotSound);
            }
            if (Physics.Raycast(_targetingObject.position, _targetingObject.forward, out var hit))
            {
                _impact.Apply(hit.transform, hit.normal, hit.point, _targetingObject.forward, _impactForce, _damage, _damageType);
            }

            _timeFromTheLastShot = 0f;
        }

    }
}

