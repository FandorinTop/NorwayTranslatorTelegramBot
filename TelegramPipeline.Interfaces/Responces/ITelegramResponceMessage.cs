using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramPipeline.Responces
{
    public interface ITelegramResponceMessage
    {
        public Task<Message> SendAsync(ITelegramBotClient sender, ChatId chatId, int? replyToMessageId = null);
    }
}