using System.Runtime.Serialization;

namespace TelegramTranslator.Common
{
    public class TGArgumentNullException : ArgumentNullException
    {
        public TGArgumentNullException()
        {
        }

        public TGArgumentNullException(string? paramName) : base(paramName)
        {
        }

        public TGArgumentNullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public TGArgumentNullException(string? paramName, string? message) : base(paramName, message)
        {
        }

        protected TGArgumentNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
