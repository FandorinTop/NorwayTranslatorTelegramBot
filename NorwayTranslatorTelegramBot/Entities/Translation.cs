using System.ComponentModel.DataAnnotations.Schema;

namespace NorwayTranslatorTelegramBot.Entities
{
    public class Translation : BaseCreationInfo
    {
        public long LeftId { get; set; }

        [ForeignKey(nameof(LeftId))]
        public Word Left { get; set; } = default!;

        public long RightId { get; set; }

        [ForeignKey(nameof(RightId))]
        public Word Right { get; set; } = default!;
    }
}
