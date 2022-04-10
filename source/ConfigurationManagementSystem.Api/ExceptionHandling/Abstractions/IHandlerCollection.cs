using System;

namespace ConfigurationManagementSystem.Api.ExceptionHandling.Abstractions
{
    internal interface IHandlerCollection
    {
        IExceptionHandler FindHandler(Exception ex);
    }
}
