using NorwayTranslatorTelegramBot.Entities;
using NorwayTranslatorTelegramBot.Translator;
using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Responces;

namespace TelegramPipeline.Middlewares
{
    public class TranslateResponceMiddleware : ITelegramMiddleware
    {
        private readonly ITranslator translator;

        public TranslateResponceMiddleware(ITranslator translator)
        {
            this.translator = translator;
        }

        public async Task InvokeAsync(TelegramContext context, Func<Task> next)
        {
            Console.WriteLine("Start " + nameof(TranslateResponceMiddleware));

            var translatedMessage = await translator.TranslateAsync(context.Request.Text!, Language.Norwegian);
            context.Add(new TelegramTranslateResponceMessage(translatedMessage.Translations.First().Text));
            Console.WriteLine("Translated to:  " + translatedMessage.Translations.First().Text);

            await next();

            Console.WriteLine("End " + nameof(TranslateResponceMiddleware));
        }
    }

}