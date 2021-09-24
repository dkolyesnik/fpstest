using System;
using Game.Components;
using UnityEngine;

namespace Game.Weapon
{
    [CreateAssetMenu(fileName = "ExplosionImpact", menuName = "ScriptableObjects/Impacts/Explosion")]
    public class ExplosionImpact : BaseImpact
    {

        [SerializeField]
        private float _radius;
        [SerializeField]
        private float _upwardsExplosionModifier = 1f;

        private Collider[] _results = new Collider[20];
        public override void Apply(Transform target, Vector3 normal, Vector3 impactPoint, Vector3 impactDirection, float impactForce, int damage, DamageType damageType)
        {
            Physics.OverlapSphereNonAlloc(impactPoint, _radius, _results);
            foreach (var collider in _results)
            {
                if (collider != null)
                {
                    var targetRigidbody = collider.GetComponent<Rigidbody>();
                    if (targetRigidbody != null)
                    {
                        targetRigidbody.AddExplosionForce(impactForce, impactPoint, _radius, _upwardsExplosionModifier, ForceMode.Impulse);
                    }
                    var hpComponent = collider.GetComponent<HPComponent>();
                    if (hpComponent != null)
                    {
                        var closestPoint = collider.ClosestPointOnBounds(impactPoint);
                        var explosionDirection = closestPoint - impactPoint;
                        var distance = explosionDirection.magnitude;
                        var distanceModifier = 1f - distance / _radius;

                        var explosionImpulceDirection = explosionDirection;
                        explosionImpulceDirection.Normalize();

                        hpComponent.ApplyDamage((int)(damage * distanceModifier), _damageType);
                    }
                }
            }
            if (_effect != null)
            {
                var explosion = _pool.Create(_effect, impactPoint, Quaternion.LookRotation(normal));
                explosion.transform.localScale = new Vector3(_radius, _radius, _radius);
            }
            Array.Clear(_results, 0, _results.Length);
        }


    }
}
