﻿using System.Runtime.Serialization;

namespace ConfigurationManagementSystem.Framework.Exceptions
{
    /// <summary>
    /// Represents errors that occur during framework initialization.
    /// </summary>
    [Serializable]
    internal class FrameworkInitializingException : Exception
    {
        public FrameworkInitializingException()
        {
        }

        public FrameworkInitializingException(string message) : base(message)
        {
        }

        public FrameworkInitializingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FrameworkInitializingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
