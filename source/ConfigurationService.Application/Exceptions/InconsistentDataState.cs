using System;

namespace ConfigurationService.Application.Exceptions
{
    public class InconsistentDataStateException : Exception
    {
        public InconsistentDataStateException() : base("") { }
        public InconsistentDataStateException(string message) : base(message) { }
    }
}
