using System;
using System.Linq.Expressions;

namespace ConfigurationManagementSystem.Api.Tests.DtoAssertions.Exceptions
{
    internal class NotFoundInValueException : Exception
    {
        private NotFoundInValueException(string message) : base(message) { }

        public static NotFoundInValueException Create(object expectedElement, Expression<Func<object>> actualPropertySelector)
        {
            if (!(actualPropertySelector.Body is MemberExpression outerMember))
            {
                throw new ApplicationException("Invalid expression");
            }

            var info = ExpressionParser.ParseMemberExpression(outerMember);
            var message = $"Element {expectedElement} not found in the {info.InnerMemberType}.{info.PropertyName} property value.";

            return new NotFoundInValueException(message);
        }
    }
}
