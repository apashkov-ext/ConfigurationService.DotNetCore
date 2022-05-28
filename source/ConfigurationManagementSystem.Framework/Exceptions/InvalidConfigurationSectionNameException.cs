using System.Runtime.Serialization;

namespace ConfigurationManagementSystem.Framework.Exceptions
{
    [Serializable]
    internal class InvalidConfigurationSectionNameException : Exception
    {
        public InvalidConfigurationSectionNameException()
        {
        }

        public InvalidConfigurationSectionNameException(string message) : base(message)
        {
        }

        public InvalidConfigurationSectionNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidConfigurationSectionNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
