using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Domain.ValueObjects.OptionValueTypes
{
    public class BooleanValue : TypedOptionValue<bool>
    {
        public BooleanValue(bool value) : base(value, OptionValueType.Boolean)
        {
        }

        protected override string ConvertToString(bool value)
        {
            return value.ToString();
        }
    }
}