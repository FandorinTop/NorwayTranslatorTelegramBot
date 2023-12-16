using NorwayTranslatorTelegramBot.Entities;
using TelegramTranslator.Dictionary.ReversoContext.HtmlParser.Interfaces;

namespace TelegramTranslator.Dictionary.ReversoContext.HtmlParser
{
    public class DictionaryTranslatorFactory : IDictionaryTranslatorFactory
    {
        public DictionaryTranslatorFactory(IEnumerable<IDictionaryTranslator> translators)
        {
            Translators = translators;
        }

        private IEnumerable<IDictionaryTranslator> Translators { get; set; }

        public IDictionaryTranslator GetTranslator(Language from, Language to)
        {
            var translator = Translators
                .FirstOrDefault(item => item.From == from && item.To == to);

            if(translator is null)
            {
                throw new Exception("No available translators");
            }

            return translator;
        }
    }
}
