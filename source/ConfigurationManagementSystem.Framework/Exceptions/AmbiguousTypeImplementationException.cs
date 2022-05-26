using System.Runtime.Serialization;

namespace ConfigurationManagementSystem.Framework.Exceptions
{
    [Serializable]
    internal class AmbiguousTypeImplementationException : Exception
    {
        public AmbiguousTypeImplementationException()
        {
        }

        public AmbiguousTypeImplementationException(string message) : base(message)
        {
        }

        public AmbiguousTypeImplementationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AmbiguousTypeImplementationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
