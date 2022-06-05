using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ConfigurationManagementSystem.Api.Extensions.ServiceCollection;

internal static partial class ServiceCollectionExtensions
{
    public static IMvcBuilder ConfigureApiBehavior(this IMvcBuilder builder)
    {
        return builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                return new BadRequestObjectResult(GetErrorBody(context.ModelState))
                {
                    ContentTypes =
                    {
                             System.Net.Mime.MediaTypeNames.Application.Json
                    }
                };
            };
        });
    }

    private static object GetErrorBody(ModelStateDictionary modelState)
    {
        var message = modelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).FirstOrDefault() ?? "";
        return new
        {
            message
        };
    }
}
