using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Domain.ValueObjects.OptionValueTypes
{
    public class StringValue : TypedOptionValue<string>
    {
        public StringValue(string value) : base(value, OptionValueType.String)
        {
        }

        protected override string ConvertToString(string value)
        {
            return value;
        }
    }
}