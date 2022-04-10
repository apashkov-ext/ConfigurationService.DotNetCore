using ConfigurationManagementSystem.Api.ExceptionHandling.Abstractions;
using ConfigurationManagementSystem.Api.ExceptionHandling.HandlerCollectionBuilding;
using ConfigurationManagementSystem.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Api.Middleware
{
    internal class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<ExceptionHandlingConfig> _options;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, 
            IOptions<ExceptionHandlingConfig> options, 
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _options = options;
            _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the api exception middleware will not be executed");
                    throw;
                }

                var handler = _options.Value.HandlerCollection?.FindHandler(e);
                if (handler == null) throw;

                _logger.LogError(0, e, "Api Exception: {message}", e.Message);
                await WriteResponseAsync(context, handler);
            }
        }

        private static async Task WriteResponseAsync(HttpContext context, IExceptionHandler handler)
        {
            context.Response.Clear();
            context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)handler.StatusCode;

            var resp = new { message = handler.Message };
            var content = JsonSerializer.Serialize(resp, SerializerOptions.JsonSerializerOptions);

            context.Response.ContentLength = Encoding.UTF8.GetByteCount(content);
            await context.Response.WriteAsync(content);
        }
    }
}
