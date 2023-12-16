using NorwayTranslatorTelegramBot.Entities;
using TelegramTranslator.Dictionary.Models;

namespace TelegramTranslator.Dictionary.Interfaces
{
    public interface IDictionaryRequestFactory
    {
        public Task<DictionaryResponce> GetDictionaryResponce(string value, Language from, Language to);
    }
}
