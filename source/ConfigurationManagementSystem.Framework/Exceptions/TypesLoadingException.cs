using System.Runtime.Serialization;

namespace ConfigurationManagementSystem.Framework.Exceptions
{
    [Serializable]
    internal class TypesLoadingException : Exception
    {
        public TypesLoadingException()
        {
        }

        public TypesLoadingException(string message) : base(message)
        {
        }

        public TypesLoadingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypesLoadingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}