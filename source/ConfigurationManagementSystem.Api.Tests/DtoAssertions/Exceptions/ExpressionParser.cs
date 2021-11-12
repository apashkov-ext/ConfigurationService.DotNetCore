using System.Linq.Expressions;
using System.Reflection;

namespace ConfigurationManagementSystem.Api.Tests.DtoAssertions.Exceptions
{
    internal static class ExpressionParser
    {
        public static MemberExpressionInfo ParseMemberExpression(MemberExpression member)
        {
            var outerProp = (PropertyInfo)member.Member;
            var innerMember = (MemberExpression)member.Expression;
            var innerField = (FieldInfo)innerMember.Member;
            var ce = (ConstantExpression)innerMember.Expression;

            var innerObj = ce.Value;
            var outerObj = innerField.GetValue(innerObj);
            var value = outerProp.GetValue(outerObj, null);

            var info = new MemberExpressionInfo(outerProp.Name, value, innerField.FieldType.Name);
            return info;
        }
    }

    internal class MemberExpressionInfo
    {
        public string PropertyName { get; }
        public object PropertyValue { get; }
        public string InnerMemberType { get; }

        public MemberExpressionInfo(string propertyName, object propertyValue, string innerMemberType)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            InnerMemberType = innerMemberType;
        }
    }
}
