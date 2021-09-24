using Framework.Core.VariableTypes.Float;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class SavableData : ScriptableObject
    {

        public FloatVariableRef MoveSpeed;
        public FloatVariableRef JumpHeight;

        public float DefaultMoveSpeed = 4f;
        public float DefautlJumpHeight = 5f;
        public PlayerData.PlayerData PlayerData;

        public void Save()
        {
            PlayerData.SetFloat(MoveSpeed.Id.name, MoveSpeed.GetValue());
            PlayerData.SetFloat(JumpHeight.Id.name, JumpHeight.GetValue());
            PlayerData.Save();
        }

        public void Load()
        {
            PlayerData.Load();
            MoveSpeed.SetValue(PlayerData.GetFloat(MoveSpeed.Id.name, DefaultMoveSpeed));
            JumpHeight.SetValue(PlayerData.GetFloat(JumpHeight.Id.name, DefautlJumpHeight));

        }
    }
}
