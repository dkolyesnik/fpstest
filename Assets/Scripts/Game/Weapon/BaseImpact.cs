using Framework.Core.VariableTypes.Object;
using Framework.Pool;
using UnityEngine;

namespace Game.Weapon
{
	public abstract class BaseImpact : ScriptableObject
	{
		[SerializeField]
		protected DamageType _damageType;
		[SerializeField]
		protected GameObject _effect;

		[SerializeField]
		protected ObjectVariableRef _poolRef;
		protected GameObjectPool _pool => _poolRef.GetValue() as GameObjectPool;
		public abstract void Apply(Transform target, Vector3 normal, Vector3 impactPoint,
			Vector3 impactDirection, float impactForce, int damage, DamageType damageType);
	}
}
