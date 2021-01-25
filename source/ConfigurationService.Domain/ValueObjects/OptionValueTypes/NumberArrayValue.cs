using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Domain.ValueObjects.OptionValueTypes
{
    public class NumberArrayValue : TypedOptionValue<int[]>
    {
        public NumberArrayValue(int[] value) : base(value, OptionValueType.NumberArray)
        {
        }

        protected override string Serialize(int[] value)
        {
            return string.Join(',', value);
        }
    }
}