using System;
using System.Net;

namespace ConfigurationManagementSystem.Api.ExceptionHandling.Abstractions
{
    internal interface IHandle<T> where T : Exception
    {
        IHandlerCollectionBuilder With(HttpStatusCode statusCode, string message);
    }
}
