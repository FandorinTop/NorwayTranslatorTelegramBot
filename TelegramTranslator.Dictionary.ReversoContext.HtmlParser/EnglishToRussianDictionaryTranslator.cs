using NorwayTranslatorTelegramBot.Entities;
using TelegramTranslator.Dictionary.ReversoContext.HtmlParser.Interfaces;

namespace TelegramTranslator.Dictionary.ReversoContext.HtmlParser
{
    public class EnglishToRussianDictionaryTranslator : BaseDictionaryTranslator, IDictionaryTranslator
    {
        public Language From => Language.English;

        public Language To => Language.Russian;

        public override string CreateTranslationPath(string value)
        {
            return $"{RequestUrl}/english-russian/{value}";
        }
    }
}
