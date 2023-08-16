namespace NorwayTranslatorTelegramBot.Translator.Azure.Languages
{
    public interface IAzureToLanguageRequestFactory
    {
        public Entities.Language ToLanguage { get; }

        public HttpRequestMessage Create(string textToTranslate);
    }
}