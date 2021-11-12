using System;
using System.Linq.Expressions;

namespace ConfigurationManagementSystem.Api.Tests.DtoAssertions.Exceptions
{
    internal class UnexpectedPropertyValueException : Exception
    {
        private UnexpectedPropertyValueException(string message) : base(message) { }

        public static UnexpectedPropertyValueException Create(object expectedValue, Expression<Func<object>> actualPropertySelector)
        {
            if (!(actualPropertySelector.Body is MemberExpression outerMember))
            {
                throw new ApplicationException("Invalid expression");
            }

            var info = ExpressionParser.ParseMemberExpression(outerMember);
            var message = $"Unexpected value of the {info.InnerMemberType}.{info.PropertyName} property.{Environment.NewLine}" +
                $"Expected: {expectedValue}{Environment.NewLine}Actual: {info.PropertyValue}";

            return new UnexpectedPropertyValueException(message);
        }
    }
}
