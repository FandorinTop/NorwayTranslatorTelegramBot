using NorwayTranslatorTelegramBot.Entities;

namespace TelegramTranslator.Dictionary.ReversoContext.HtmlParser.Interfaces
{
    public interface IDictionaryTranslator
    {
        public Language From { get; }

        public Language To { get; }

        public string CreateTranslationPath(string value);
    }
}
