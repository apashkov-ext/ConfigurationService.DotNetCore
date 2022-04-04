using System;
using System.Runtime.Serialization;

namespace ConfigurationManagementSystem.ServicesConfiguring.Exceptions
{
    [Serializable]
    internal class ConfigurationBindingException : Exception
    {
        public ConfigurationBindingException()
        {
        }

        public ConfigurationBindingException(string message) : base(message)
        {
        }

        public ConfigurationBindingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigurationBindingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}