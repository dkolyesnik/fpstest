using Game.Components;
using UnityEngine;

namespace Game.Weapon
{
    [CreateAssetMenu(fileName = "Impact", menuName = "ScriptableObjects/Impacts/Bullet")]
    public class BulletImpact : BaseImpact
    {
        public override void Apply(Transform target, Vector3 normal, Vector3 impactPoint, Vector3 impactDirection, float impactForce, int damage, DamageType damageType)
        {
            var hpComponent = target.transform.GetComponent<HPComponent>();
            if (hpComponent != null)
            {
                hpComponent.ApplyDamage(damage, _damageType);
            }
            var targetRigidbody = target.transform.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                impactDirection.Normalize();
                targetRigidbody.AddForceAtPosition(impactDirection * impactForce, impactPoint, ForceMode.Impulse);
            }
            if (_effect != null)
            {
                _pool.Create(_effect, impactPoint, Quaternion.LookRotation(normal));
            }
        }
    }
}
