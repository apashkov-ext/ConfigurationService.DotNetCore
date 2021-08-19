using ConfigurationService.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

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
                case NotFoundException ex:
                
                    context.Result = new ObjectResult(new { message = ex.Message })
                    {
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                    context.ExceptionHandled = true;
                    break;
                
                case AlreadyExistsException ex:
                
                    context.Result = new ObjectResult(new { message = ex.Message })
                    {
                        StatusCode = (int)HttpStatusCode.UnprocessableEntity
                    };
                    context.ExceptionHandled = true;
                    break;
                
                case InconsistentDataStateException ex:
                
                    context.Result = new ObjectResult(new { message = ex.Message })
                    {
                        StatusCode = (int)HttpStatusCode.UnprocessableEntity
                    };
                    context.ExceptionHandled = true;
                    break;
                
                case InternalException ex:
                
                    context.Result = new ObjectResult(new { message = ex.Message })
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    context.ExceptionHandled = true;
                    break;

                case { } ex:
                    context.Result = new ObjectResult(new { message = ex.Message })
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }
}
