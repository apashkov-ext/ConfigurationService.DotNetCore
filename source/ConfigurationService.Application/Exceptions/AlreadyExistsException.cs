using System;

namespace ConfigurationService.Application.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException() : base("") { }
        public AlreadyExistsException(string message) : base(message) { }
    }
}
