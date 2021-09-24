using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core
{
    public abstract class Variable : ScriptableObject
    {
        public abstract VariableId VariableId { get; }
        [SerializeField]
        protected bool IsReadonly = false;

        internal abstract void SetVariableId(VariableId variableId);
    }
    public abstract class Variable<TValue> : Variable
    {
        [SerializeField]
        protected TValue _value;

        public event Action<TValue> Changed;
        public TValue GetValue()
        {
            return _value;
        }
        public void SetValue(TValue value)
        {
            if (IsReadonly)
            {
                Debug.LogWarning("Variable is readonly ScriptableObjectName=" + this.name);
                return;
            }
            _value = value;
            Changed?.Invoke(_value);
        }

    }

    public abstract class Variable<TValue, TVariable, TVariableId> : Variable<TValue>
        where TVariableId : VariableId<TVariable>
        where TVariable : Variable<TValue>
    {
        public override VariableId VariableId
        {
            get { return _variableId; }
        }
        internal override void SetVariableId(VariableId variableId)
        {
            _variableId = (TVariableId)variableId;
            name = variableId.name;
        }

        [SerializeField]
        protected TVariableId _variableId;

    }
}