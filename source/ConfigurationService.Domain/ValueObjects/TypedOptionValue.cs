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
            return ConvertToString((T)value);
        }

        protected abstract string ConvertToString(T value);
    }
}