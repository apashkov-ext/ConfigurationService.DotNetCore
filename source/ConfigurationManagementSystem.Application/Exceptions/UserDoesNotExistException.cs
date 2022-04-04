using System;
using System.Runtime.Serialization;

namespace ConfigurationManagementSystem.Application.Exceptions
{
    [Serializable]
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException() { }
        public UserDoesNotExistException(string message) : base(message) { }
        public UserDoesNotExistException(string message, Exception inner) : base(message, inner) { }
        protected UserDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
