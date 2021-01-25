using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Domain.ValueObjects.OptionValueTypes
{
    public class StringArrayValue : TypedOptionValue<string[]>
    {
        public StringArrayValue(string[] value) : base(value, OptionValueType.StringArray)
        {
        }

        protected override string Serialize(string[] value)
        {
            return string.Join(',', value);
        }
    }
}