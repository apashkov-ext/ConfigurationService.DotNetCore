using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Domain.ValueObjects
{
    public abstract class TypedOptionValue<T> : OptionValue
    {
        protected TypedOptionValue(T value, OptionValueType type) : base(value, type)
        {
        }

        protected override string ConvertToString(object value)
        {
            return Serialize((T)value);
        }

        protected abstract string Serialize(T value);
    }
}