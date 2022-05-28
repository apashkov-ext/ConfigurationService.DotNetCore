using System.Runtime.Serialization;

namespace ConfigurationManagementSystem.Framework.Exceptions
{
    [Serializable]
    internal class AssemblyLoadingException : Exception
    {
        public AssemblyLoadingException()
        {
        }

        public AssemblyLoadingException(string message) : base(message)
        {
        }

        public AssemblyLoadingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AssemblyLoadingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}