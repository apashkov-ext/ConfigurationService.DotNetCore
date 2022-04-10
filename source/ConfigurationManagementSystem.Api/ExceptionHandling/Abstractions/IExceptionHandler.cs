using System;
using System.Net;

namespace ConfigurationManagementSystem.Api.ExceptionHandling.Abstractions
{
    /// <summary>
    /// Describes exception handler behavior.
    /// </summary>
    internal interface IExceptionHandler
    {
        HttpStatusCode StatusCode { get; }
        string Message { get; }
        bool CanHandle<Ex>(Ex ex) where Ex : Exception;
    }
}
