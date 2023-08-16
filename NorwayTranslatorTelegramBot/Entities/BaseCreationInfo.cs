namespace NorwayTranslatorTelegramBot.Entities
{
    public class BaseCreationInfo
    {
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; }
    }
}
