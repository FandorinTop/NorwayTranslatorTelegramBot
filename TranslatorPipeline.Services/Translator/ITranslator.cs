using NorwayTranslatorTelegramBot.Entities;
using NorwayTranslatorTelegramBot.ViewModel.Translator;

namespace NorwayTranslatorTelegramBot.Translator
{
    public interface ITranslator
    {
        public Task<TranslationResult> TranslateAsync(string text, Language toLanguage);
    }
}