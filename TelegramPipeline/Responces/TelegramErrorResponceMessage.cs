using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramPipeline.Responces
{
    public class TelegramErrorResponceMessage : ITelegramResponceMessage
    {
        public string errorMessage { get; }

        public TelegramErrorResponceMessage(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        public Task<Message> SendAsync(ITelegramBotClient sender, ChatId chatId, int? replyToMessageId = null)
        {
            return sender.SendTextMessageAsync(chatId, errorMessage, replyToMessageId: replyToMessageId);
        }
    }
}