using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ConfigurationManagementSystem.Persistence.ContextConfiguration
{
    internal static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<DateTime> DateTimeConversion(this PropertyBuilder<DateTime> builder)
        {
            return builder;
            //return builder.HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
    }
}
