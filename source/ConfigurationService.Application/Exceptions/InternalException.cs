using System;

namespace ConfigurationService.Application.Exceptions
{
    public class InternalException : Exception
    {
        public InternalException() : base("") { }
        public InternalException(string message) : base(message) { }
    }
}
