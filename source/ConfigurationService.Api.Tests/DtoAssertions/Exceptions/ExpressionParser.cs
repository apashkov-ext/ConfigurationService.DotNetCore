using System.Linq.Expressions;
using System.Reflection;

namespace ConfigurationService.Api.Tests.DtoAssertions.Exceptions
{
    internal static class ExpressionParser
    {
        public static MemberExpressionInfo ParseMemberExpression(MemberExpression member)
        {
            PropertyInfo outerProp = (PropertyInfo)member.Member;
            MemberExpression innerMember = (MemberExpression)member.Expression;
            FieldInfo innerField = (FieldInfo)innerMember.Member;
            ConstantExpression ce = (ConstantExpression)innerMember.Expression;

            var innerObj = ce.Value;
            var outerObj = innerField.GetValue(innerObj);
            object value = outerProp.GetValue(outerObj, null);

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
