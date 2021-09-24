using System;
using UnityEngine;

namespace Framework.Core
{
    [Serializable]
    public class ContextRef
    {
        //TODO property drawer to select what to use, and Add a default Context as the default value

        [Tooltip("Will use reference if not null, if null will try to find by ContextId")]
        [SerializeField]
        private Context Reference;
        [SerializeField]
        private ContextId ContextId;
        private Context _context;
        private Context GetContext()
        {
            return _context ?? CreateContext();
        }

        private Context CreateContext()
        {
            if (Reference != null)
            {
                _context = Reference;
                return _context;
            }
            if (ContextId != null)
            {
                // Debug.Log($"ScriptableObject name class = {ContextId.GetType().FullName} name = {ContextId.name}");
                var go = GameObject.Find(ContextId.name);
                if (go == null)
                {
                    if (ContextId.DefaultContextPrefab == null)
                    {
                        Debug.LogError($"Default implementation of Context with Id {ContextId.name} is not specified in ContextId");
                        return null;
                    }
                    else
                    {
                        go = GameObject.Instantiate(ContextId.DefaultContextPrefab.gameObject) as GameObject;
                        go.name = ContextId.name;

                    }
                }
                _context = go.GetComponent<Context>();
                return _context;
            }
            else
            {
                throw new Exception("ContextId is not specified");
            }
        }

        internal Variable<T> GetVariable<T>(VariableId variableId)
        {
            return GetContext().GetVariable<T>(variableId);
        }

        public override string ToString()
        {
            if (Reference != null)
            {
                return "Context gameObject: " + Reference.gameObject.name;
            }
            if (ContextId != null)
            {
                return "GlobalContextId: " + ContextId.name;
            }
            return "Wrong Context";
        }
    }
}


// public abstract class ContextSource : ScriptableObject
// {
//     public abstract Context GetContext();
// }

// public class GlobalContextSource : ContextSource
// {
//     public ContextId ContextId;

// }

// [CreateAssetMenu]
// public class ReferenceContextSource : ContextSource
// {
//     public Context Reference;
//     public override Context GetContext()
//     {
//         if (Reference == null)
//         {
//             Debug.LogError("Reference is not specified for the context");
//         }
//         return Reference;
//     }
// }