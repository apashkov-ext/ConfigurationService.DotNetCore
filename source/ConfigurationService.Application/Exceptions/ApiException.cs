using System;

namespace ConfigurationService.Application.Exceptions
{
    public class ApiException : ApplicationException
    {
        public int StatusCode { get; }

        public ApiException() {}
        public ApiException(string message) : base(message) {}
        public ApiException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}