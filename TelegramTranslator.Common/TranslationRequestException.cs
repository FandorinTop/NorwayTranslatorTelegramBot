using System.Net;

namespace TelegramTranslator.Common
{
    public class TranslationRequestException : Exception
    {
        public TranslationRequestException(
            string exceptionMessage,
            HttpStatusCode responseCode,
            string requestUri,
            string responceAsText
            )
            : base(exceptionMessage)
        {
            ResponseCode = responseCode;
            RequestUri = requestUri ?? throw new ArgumentNullException(nameof(requestUri));
            ResponceAsText = responceAsText ?? throw new ArgumentNullException(nameof(responceAsText));
        }

        public HttpStatusCode ResponseCode { get; }
        public string RequestUri { get; }
        public string ResponceAsText { get; }
    }
}
