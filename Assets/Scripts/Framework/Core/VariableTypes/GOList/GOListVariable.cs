using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core.VariableTypes.GOList
{
    [CreateAssetMenu(fileName = "Vairable", menuName = "Variables/GOList")]
    public class GOListVariable : Variable<List<GameObject>, GOListVariable, GOListVariableId>
    {
        internal void InitializeList()
        {
            IsReadonly = true;
            _value = new List<GameObject>();
        }
    }
}
