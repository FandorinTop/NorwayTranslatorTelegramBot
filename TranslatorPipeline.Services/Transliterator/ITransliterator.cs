using NorwayTranslatorTelegramBot.Entities;

namespace NorwayTranslatorTelegramBot.Transliterator
{
    public interface ITransliterator
    {
        Task TransliterateAsync(string text, Language language);
    }
}
