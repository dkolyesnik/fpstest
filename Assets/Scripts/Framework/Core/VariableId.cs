using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core
{
    public abstract class VariableId : ScriptableObject
    {
        public abstract Variable CreateVariable();
    }

    public abstract class VariableId<T> : VariableId where T : Variable
    {
        public override Variable CreateVariable()
        {
            var variable = ScriptableObject.CreateInstance<T>();
            variable.SetVariableId(this);
            return variable;
        }
    }
}