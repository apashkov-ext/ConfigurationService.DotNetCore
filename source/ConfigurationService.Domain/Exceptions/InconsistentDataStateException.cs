using System;

namespace ConfigurationService.Domain.Exceptions
{
    public class InconsistentDataStateException : Exception
    {
        public InconsistentDataStateException() : base("") { }
        public InconsistentDataStateException(string message) : base(message) { }
    }
}
