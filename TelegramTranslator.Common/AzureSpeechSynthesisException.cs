namespace TelegramTranslator.Common
{
    public class AzureSpeechSynthesisException : Exception
    {
        public AzureSpeechSynthesisException(
            string message,
            string errorCode,
            string errorDetail,
            string resultReason = "Canceled"
            ) : base(message)
        {
            ErrorCode = errorCode;
            ErrorDetail = errorDetail;
            ResultReason = resultReason;
        }


        public string ResultReason { get; }
        public string ErrorCode { get; }
        public string ErrorDetail { get; }
    }
}
