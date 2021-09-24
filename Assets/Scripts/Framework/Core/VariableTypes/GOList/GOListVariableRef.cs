using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core.VariableTypes.GOList
{
	[Serializable]
	public class GOListVariableRef : VariableRef<List<GameObject>, GOListVariableId, GOListVariable> { }
}
