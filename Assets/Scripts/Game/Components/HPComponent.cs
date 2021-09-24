using System.Collections.Generic;
using Framework.Core.VariableTypes.Int;
using UnityEngine;

namespace Game.Components
{
    public class HPComponent : MonoBehaviour
    {
        public bool IsDead { get; private set; }

        [SerializeField]
        private List<SOAction> _onDeathActions;

        //TODO remove and use event
        [SerializeField]
        private IntVariableRef _scores;

        [SerializeField]
        private IntVariableRef _currentHP;
        [SerializeField]
        private IntVariableRef _maxHP;

        // Start is called before the first frame update
        private void Awake()
        {

            _currentHP.SetValue(_maxHP.GetValue());
        }
        public void ApplyDamage(int damage, DamageType damageType)
        {
            if (IsDead)
            {
                return;
            }
            Debug.Log($"{gameObject.name} is damaged by {damage} of type {damageType.name}");
            var _currentHPValue = _currentHP.GetValue();
            _currentHPValue -= damage;
            _currentHP.SetValue(_currentHPValue);

            if (_currentHPValue <= 0)
            {
                _scores.SetValue(_scores.GetValue() + damageType.Score);
                Die();
            }
        }

        private void Die()
        {
            IsDead = true;
            foreach (var action in _onDeathActions)
            {
                action.Apply(gameObject);
            }
            //TODO return to pool
        }


        // public void Reset()
        // {
        //     _currentHp = _maxHp;
        //     IsDead = false;
        // }
    }
}
