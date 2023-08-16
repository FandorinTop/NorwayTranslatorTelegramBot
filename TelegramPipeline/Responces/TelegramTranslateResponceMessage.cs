using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramPipeline.Responces
{
    public class TelegramTranslateResponceMessage : ITelegramResponceMessage
    {
        public TelegramTranslateResponceMessage(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public Task<Message> SendAsync(ITelegramBotClient sender, ChatId chatId, int? replyToMessageId = null)
        {
            return sender.SendTextMessageAsync(chatId, Text, replyToMessageId: replyToMessageId);
        }
    }
}