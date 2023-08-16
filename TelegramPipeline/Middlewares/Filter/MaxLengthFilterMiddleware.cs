using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Responces;

namespace TelegramPipeline.Middlewares.Filter
{
    public class MaxLengthFilterMiddleware : ITelegramMiddleware
    {
        public const int MaxLength = 500;

        public async Task InvokeAsync(TelegramContext context, Func<Task> next)
        {
            Console.WriteLine("Start " + nameof(MaxLengthFilterMiddleware));

            if (context.Request.Text!.Length > MaxLength)
            {
                context.Add(new TelegramErrorResponceMessage($"Send text that has less than {MaxLength} characters"));
                Console.WriteLine($"Text larger than {MaxLength}");
                return;
            }

            await next();

            Console.WriteLine("End " + nameof(MaxLengthFilterMiddleware));
        }
    }
}