using UnityEngine;

namespace Framework.Core.VariableTypes.Object
{
	[CreateAssetMenu(fileName = "Vairable", menuName = "Variables/Object")]
	public class ObjectVariable : Variable<UnityEngine.Object, ObjectVariable, ObjectVariableId>
	{
	}
}
