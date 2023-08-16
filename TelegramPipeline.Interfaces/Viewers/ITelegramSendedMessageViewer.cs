using Telegram.Bot.Types;
using TelegramPipeline.Responces;

namespace TelegramPipeline.Viewers
{
    public interface ITelegramSendedMessageViewer
    {
        public Task ViewResultAsync(Message request, IReadOnlyDictionary<ITelegramResponceMessage, Message> dictionary);
    }
}