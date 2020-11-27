using ConfigurationService.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ConfigurationService.Api.Filters
{
    public class HttpExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            switch (context.Exception)
            {
                case ApiException ex:
                
                    context.Result = new ObjectResult(new { message = ex.Message })
                    {
                        StatusCode = ex.StatusCode
                    };
                    context.ExceptionHandled = true;
                    break;

                case { } ex:
                    context.Result = new ObjectResult(new { message = ex.Message })
                    {
                        StatusCode = 500
                    };
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }
}
