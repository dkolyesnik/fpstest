using System.Collections;
using System.Collections.Generic;
using Framework.Core.VariableTypes.Object;
using Framework.WindowManager;
using UnityEngine;

namespace Game
{

    public class Starter : MonoBehaviour
    {
        [SerializeField] private WindowId startingWindowId;

        [SerializeField] private ObjectVariableRef _windowManagerRef;
        private WindowManager _windowManager => _windowManagerRef.GetValue() as WindowManager;

        [SerializeField] private ObjectVariableRef _savableDataRef;
        private SavableData _savableData => _savableDataRef.GetValue() as SavableData;

        void Start()
        {
            _savableData.Load();
            _windowManager.Show(startingWindowId);
        }

    }
}