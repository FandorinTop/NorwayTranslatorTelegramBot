namespace NorwayTranslatorTelegramBot.TextToSpeech
{
    public interface ITextToSpeechService
    {
        public Task<byte[]> GetSpeechBytesAsync(string text);
    }
}