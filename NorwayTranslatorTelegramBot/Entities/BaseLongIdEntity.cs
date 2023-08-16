using System.ComponentModel.DataAnnotations;

namespace NorwayTranslatorTelegramBot.Entities
{
    public class BaseLongIdEntity : BaseCreationInfo
    {
        [Key]
        public long Id { get; set; }
    }
}
