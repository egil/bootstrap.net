using System;
using System.Runtime.Serialization;

namespace Egil.RazorComponents.Bootstrap.Base
{
    [Serializable]
    internal class InvalidChildContentException : Exception
    {
        public InvalidChildContentException()
        {
        }

        public InvalidChildContentException(string message) : base(message)
        {
        }

        public InvalidChildContentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidChildContentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}