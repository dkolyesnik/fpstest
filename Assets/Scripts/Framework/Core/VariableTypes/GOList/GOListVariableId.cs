using UnityEngine;

namespace Framework.Core.VariableTypes.GOList
{
    [CreateAssetMenu(fileName = "Vairable", menuName = "Variables/Event")]
    public class GOListVariableId : VariableId<GOListVariable>
    {
        public override Variable CreateVariable()
        {
            var variable = ScriptableObject.CreateInstance<GOListVariable>();
            variable.SetVariableId(this);
            variable.InitializeList();
            return variable;
        }
    }
}
