using ConfigurationManagementSystem.Api.ExceptionHandling.HandlerCollectionBuilding;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConfigurationManagementSystem.Api.Middleware
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddExceptionHandling(this IServiceCollection serviceCollection, Action<ExceptionHandlingConfig> builder)
        {
            serviceCollection.Configure(builder);
        }
    }
}
