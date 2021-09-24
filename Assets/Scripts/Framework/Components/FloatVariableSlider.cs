using Framework.Core.VariableTypes.Float;

namespace Framework.Components
{
    public class FloatVariableSlider : VariableSlider<float, FloatVariable, FloatVariableId, FloatVariableRef>
    {
        protected override float ConvertFloatToVariableValue(float value)
        {
            return value;
        }

        protected override float ConvertVariableValueToFloat(float value)
        {
            return value;
        }
    }
}
