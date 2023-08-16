using Microsoft.EntityFrameworkCore;
using NorwayTranslatorTelegramBot.Entities;
using System.ComponentModel.DataAnnotations;

namespace CacheEntities
{
    [Index(nameof(Text))]
    public class CacheTgMessage
    {
        [Key]
        public long CacheId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; } = default!;

        [MaxLength(100)]
        public string FromLanguage { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string TranslatedText { get; set; } = default!;

        public Language ToLanguage { get; set; } = Language.Norwegian;

        [Required]
        [MaxLength(300)]
        public string TelegramFileId { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int Version { get; set; } = 0;
    }
}