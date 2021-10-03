using System;

namespace ConfigurationManagementSystem.Domain.Exceptions
{
    public class InconsistentDataStateException : Exception
    {
        public InconsistentDataStateException() : base("") { }
        public InconsistentDataStateException(string message) : base(message) { }
    }
}
