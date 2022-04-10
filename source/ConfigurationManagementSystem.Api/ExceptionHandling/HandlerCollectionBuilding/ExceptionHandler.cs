using ConfigurationManagementSystem.Api.ExceptionHandling.Abstractions;
using System;
using System.Net;

namespace ConfigurationManagementSystem.Api.ExceptionHandling.HandlerCollectionBuilding
{
    internal class ExceptionHandler<T> : IExceptionHandler, IHandle<T> where T : Exception
    {
        private readonly HandlerCollectionBuilder _exceptionHandlerBuilder;

        public HttpStatusCode StatusCode { get; private set; }
        public string Message { get; private set; }

        public ExceptionHandler(HandlerCollectionBuilder exceptionHandlerBuilder)
        {
            _exceptionHandlerBuilder = exceptionHandlerBuilder;
        }

        public bool CanHandle<Ex>(Ex ex) where Ex : Exception
        {
            return ex is T;
        }

        public IHandlerCollectionBuilder With(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;

            _exceptionHandlerBuilder.AddHandler(this);
            return _exceptionHandlerBuilder;
        }
    }
}
