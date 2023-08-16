using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramPipeline.Responces
{
    public class TelegramErrorResponceMessage : ITelegramResponceMessage
    {
        public string ErrorMessage { get; }

        public TelegramErrorResponceMessage(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public Task<Message> SendAsync(ITelegramBotClient sender, ChatId chatId, int? replyToMessageId = null)
        {
            return sender.SendTextMessageAsync(chatId, ErrorMessage, replyToMessageId: replyToMessageId);
        }
    }
}