using Game.Weapon;
using UnityEngine;

namespace Game.SOActions
{
    [CreateAssetMenu(fileName = "Action", menuName = "ScriptableObjects/Actions/Explosion")]
    public class ExplosionSOAction : SOAction
    {
        public float Force;
        public int Damage;
        public DamageType DamageType;
        public BaseImpact Impact;
        public override void Apply(GameObject go)
        {
            if (Impact != null)
            {
                Impact.Apply(go.transform, Vector3.up, go.transform.position, Vector3.up, Force, Damage, DamageType);
            }
        }
    }
}
