using System;
using System.Runtime.Serialization;

namespace ConfigurationManagementSystem.ServicesConfiguring.Exceptions
{
    [Serializable]
    internal class ImplementationNotFoundException : Exception
    {
        public ImplementationNotFoundException()
        {
        }

        public ImplementationNotFoundException(string message) : base(message)
        {
        }

        public ImplementationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImplementationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}