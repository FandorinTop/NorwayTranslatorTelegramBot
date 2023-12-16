using NorwayTranslatorTelegramBot.Entities;

namespace TelegramTranslator.Dictionary.Models
{
    public class DictionaryResponce
    {
        public string Input { get; set; } = default!;

        public Language From { get; set; } = default!;

        public IEnumerable<string> Suggestion { get; set; } = new List<string>();

        public Language To { get; set; } = default!;

        public IEnumerable<PartOfSpeechResponce> Results { get; set; } = new List<PartOfSpeechResponce>();
    }
}
