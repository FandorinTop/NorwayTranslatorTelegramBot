using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramPipeline.Responces
{
    public class TelegramAudioResponceMessage : ITelegramResponceMessage
    {
        public TelegramAudioResponceMessage(InputFile inputFile)
        {
            InputFile = inputFile;
        }

        private InputFile InputFile { get; }

        public async Task<Message> SendAsync(ITelegramBotClient sender, ChatId chatId, int? replyToMessageId = null)
        {
            return await sender.SendAudioAsync(chatId, InputFile, replyToMessageId: replyToMessageId);
        }
    }
}