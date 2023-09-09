using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TelegramPipeline.Pipelines;
using TelegramPipeline.Responces;
using TelegramPipeline.Viewers;

namespace TelegramPipeline
{
    public class MessageHandler : IUpdateHandler
    {
        private readonly TelegramMessagePipeline pipeline;
        private readonly IEnumerable<ITelegramSendedMessageViewer> messageViewers;

        public MessageHandler(TelegramMessagePipeline pipeline,
            IEnumerable<ITelegramSendedMessageViewer> messageViewers = null!
            )
        {
            this.pipeline = pipeline;
            this.messageViewers = messageViewers ?? new List<ITelegramSendedMessageViewer>();
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                NullReferenceException nullReferenceException => $"Telegram NullReference Exception:\n{nullReferenceException.Message}",
                _ => exception.ToString(),
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                var request = update.Message;

                if (request is null)
                    return;

                var context = new TelegramContext(request);
                await pipeline.ProcessMessageAsync(context, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();

                var resultDictionary = await SendMessagesAsync(botClient, context);

                cancellationToken.ThrowIfCancellationRequested();

                if (resultDictionary.Any())
                    foreach (var viewer in messageViewers)
                        await viewer.ViewResultAsync(request, resultDictionary);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private async Task<IReadOnlyDictionary<ITelegramResponceMessage, Message>> SendMessagesAsync(ITelegramBotClient botClient, TelegramContext context)
        {
            Dictionary<ITelegramResponceMessage, Message> sendedMessages = new Dictionary<ITelegramResponceMessage, Message>();

            Message preview = context.Request;

            foreach (var responce in context)
            {
                preview = await responce.SendAsync(botClient, new ChatId(context.Request.Chat.Id), preview?.MessageId);
                sendedMessages.Add(responce, preview);
            }

            return sendedMessages;
        }
    }
}