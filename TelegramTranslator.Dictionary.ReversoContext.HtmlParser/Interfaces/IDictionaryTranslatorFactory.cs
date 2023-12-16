using NorwayTranslatorTelegramBot.Entities;

namespace TelegramTranslator.Dictionary.ReversoContext.HtmlParser.Interfaces
{
    public interface IDictionaryTranslatorFactory
    {
        public IDictionaryTranslator GetTranslator(Language from, Language to);
    }
}
