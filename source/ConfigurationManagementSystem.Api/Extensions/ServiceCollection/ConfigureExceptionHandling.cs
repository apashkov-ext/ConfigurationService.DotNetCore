using ConfigurationManagementSystem.Api.ExceptionHandling.HandlerCollectionBuilding;
using ConfigurationManagementSystem.Api.Middleware;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace ConfigurationManagementSystem.Api.Extensions.ServiceCollection;

internal static partial class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandling(x =>
        {
            x.HandlerCollection = HandlerCollectionBuilder.Create()

            .Handle<InvalidPasswordException>()
            .With(HttpStatusCode.Unauthorized, "Incorrect username or password")

            .Handle<UserNotFoundException>()
            .With(HttpStatusCode.Unauthorized, "Incorrect username or password")

            .Handle<EntityNotFoundException>()
            .With(HttpStatusCode.NotFound, "Entity not found")

            .Handle<AlreadyExistsException>()
            .With(HttpStatusCode.UnprocessableEntity, "Entity already exists")

            .Handle<InconsistentDataStateException>()
            .With(HttpStatusCode.UnprocessableEntity, "Invalid entity operation")

            .HandleByDefault()
            .With(HttpStatusCode.InternalServerError, "Internal server error")

            .BuildCollection();

        });

        return services;
    }
}
