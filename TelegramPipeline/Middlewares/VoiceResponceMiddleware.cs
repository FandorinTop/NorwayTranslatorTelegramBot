using NorwayTranslatorTelegramBot.TextToSpeech;
using Telegram.Bot.Types;
using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Responces;
using TelegramTranslator.Common;

namespace TelegramPipeline.Middlewares
{
    public class VoiceResponceMiddleware : ITelegramMiddleware
    {
        private readonly ITextToSpeechService speechSyntez;

        public VoiceResponceMiddleware(ITextToSpeechService speechSyntez)
        {
            this.speechSyntez = speechSyntez;
        }

        public async Task InvokeAsync(TelegramContext context, Func<Task> next)
        {
            InputFile inputFile = default!;
            Console.WriteLine("Start " + nameof(VoiceResponceMiddleware));
            var lastTextMessage = context.PeekLastOfType<TelegramTranslateResponceMessage>()?.Text;

            if (string.IsNullOrEmpty(lastTextMessage))
            {
                throw new TGArgumentNullException($"No translation for Voice synthezing, add before voicing middleware wich create translation of type: '{nameof(TelegramTranslateResponceMessage)}'");
            }

            Console.WriteLine("Speech synthething: " + lastTextMessage);
            var byteArray = await speechSyntez.GetSpeechBytesAsync(lastTextMessage);

            inputFile = InputFile.FromStream(new MemoryStream(byteArray));
            context.Add(new TelegramAudioResponceMessage(inputFile));

            await next();

            Console.WriteLine("End " + nameof(VoiceResponceMiddleware));
        }
    }
}