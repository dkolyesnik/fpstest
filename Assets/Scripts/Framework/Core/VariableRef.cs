using System;
using UnityEngine;

namespace Framework.Core
{
    public abstract class VariableRef<T, TVariableId, TVariable>
        where TVariableId : VariableId<TVariable>
        where TVariable : Variable<T>
    {
        public TVariableId Id;
        public ContextRef Context;

        public event Action<T> Changed
        {
            add
            {
                CheckVariable();
                _variable.Changed += value;
            }
            remove
            {
                CheckVariable();
                _variable.Changed -= value;
            }
        }

        protected Variable<T> _variable;
        public T GetValue()
        {
            CheckVariable();
            return _variable.GetValue();
        }


        public void SetValue(T value)
        {
            CheckVariable();
            _variable.SetValue(value);
        }

        private void CheckVariable()
        {
            if (_variable == null)
            {
                if (Id == null)
                    throw new Exception($"VariableId is null Context is: Context={Context.ToString()}");
                
                _variable = Context.GetVariable<T>(Id);
                
                if (_variable == null)
                    Debug.LogWarning("Variable is not found");
            }
        }


    }
}

