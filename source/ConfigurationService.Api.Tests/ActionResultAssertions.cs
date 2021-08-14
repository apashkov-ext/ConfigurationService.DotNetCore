using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System;

namespace ConfigurationService.Api.Tests
{
    internal static class ActionResultAssertions
    {
        public static void IsAssignableFrom<T>(ActionResult<T> result)
        {
            Xunit.Assert.IsAssignableFrom<T>(result.Value);
        }

        public static void ResultValueIsNotNull<T>(ActionResult<T> result)
        {
            var model = result.Value;
            Xunit.Assert.NotNull(model);
        }

        public static void ContainsValue<T, TProperty>(ActionResult<T> result, Expression<Func<T, TProperty>> property, TProperty expectedValue) where T : class
        {
            var model = result.Value;

            var body = (MemberExpression)property.Body;
            var name = body.Member.Name;

            var t = typeof(T);
            var prop = t.GetProperty(name);
            var v = prop.GetValue(model);

            Xunit.Assert.Equal(expectedValue, v);
        }
    }
}
