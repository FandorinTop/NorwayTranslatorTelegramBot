namespace NorwayTranslatorTelegramBot.ViewModel.Translator
{
    public class TranslationResult
    {
        public DetectedLanguage DetectedLanguage { get; set; } = new DetectedLanguage();

        public List<Translation> Translations { get; set; } = new List<Translation>();
    }
}