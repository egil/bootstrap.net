using System;
using System.Runtime.Serialization;

namespace Egil.RazorComponents.Bootstrap.Services.PageVisibilityAPI
{
    [Serializable]
    internal class PageVisibilityApiInteropException : Exception
    {
        public PageVisibilityApiInteropException()
        {
        }

        public PageVisibilityApiInteropException(string message) : base(message)
        {
        }

        public PageVisibilityApiInteropException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PageVisibilityApiInteropException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}