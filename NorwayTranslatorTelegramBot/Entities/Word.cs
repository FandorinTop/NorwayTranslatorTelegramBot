using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NorwayTranslatorTelegramBot.Entities
{
    [Index(nameof(Value))]
    public class Word : BaseLongIdEntity
    {
        [Required]
        [MaxLength(255)]
        public string Value { get; set; } = default!;

        [MaxLength(600)]
        public string? Decription { get; set; } = default!;

        public List<PartOfSpeech> PartOfSpeeches { get; set; } = new List<PartOfSpeech>();
    }
}
