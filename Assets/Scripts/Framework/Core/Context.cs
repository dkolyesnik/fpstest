using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Core
{
    public class Context : MonoBehaviour
    {
        // public ContextRef ParentContext;
        //TODO custom property drawer
        public bool VerboseAutoceation = true;
        public List<VariableRecord> Variables;
        internal Variable<T> GetVariable<T>(VariableId variableId)
        {
            var variableRecord = Variables.FirstOrDefault(x => x.Id == variableId);
            if (variableRecord == null)
            {
                //TODO add search in parent if not found
                if (VerboseAutoceation)
                    Debug.Log($"Variable {variableId.name} is created in Contex in gameObject {gameObject.name}");
                
                variableRecord = new VariableRecord(variableId);

                Variables.Add(variableRecord);
            }
            return (Variable<T>)variableRecord.GetVariable();
        }
    }


//TODO use custom drawer
    [Serializable]
    public class VariableRecord
    {
        public VariableId Id
        {
            get
            {
                if (_variable != null)
                {
                    return _variable.VariableId;
                }
                else
                {
                    throw new Exception("VariableRecord is not filled");
                }
            }
        }
        [SerializeField]
        private Variable _variable;
        [SerializeField]

        public VariableRecord(VariableId variableId)
        {
            // _id = variableId;
            _variable = variableId.CreateVariable();
        }

        public VariableRecord(Variable variable)
        {
            _variable = variable;
        }

        public Variable GetVariable()
        {
            if (_variable == null)
            {
                // if (_id == null)
                // {
                throw new Exception("VariableRecord is not filled");
                // }
                // _variable = _id.CreateVariable();
            }
            return _variable;
        }
    }
}