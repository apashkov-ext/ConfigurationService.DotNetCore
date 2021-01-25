using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Domain.ValueObjects.OptionValueTypes
{
    public class NumberValue : TypedOptionValue<int>
    {
        public NumberValue(int value) : base(value, OptionValueType.Number)
        {
        }

        protected override string Serialize(int value)
        {
            return value.ToString();
        }
    }
}