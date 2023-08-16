using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TelegramTranslator.Common;

namespace NorwayTranslatorTelegramBot.Entities
{
    [Index(nameof(Value), nameof(Language))]
    public class PartOfSpeech : BaseLongIdEntity
    {
        public Language Language { get; set; }

        [Required]
        [MaxLength(255)]
        public string Value { get; set; } = default!;

        public List<Word> Words { get; set; } = new List<Word>();
    }
}
