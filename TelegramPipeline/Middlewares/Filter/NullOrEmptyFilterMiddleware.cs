using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Responces;

namespace TelegramPipeline.Middlewares.Filter
{
    public class NullOrEmptyFilterMiddleware : ITelegramMiddleware
    {
        public async Task InvokeAsync(TelegramContext context, Func<Task> next)
        {
            if (string.IsNullOrEmpty(context.Request.Text))
            {
                context.Add(new TelegramErrorResponceMessage($"Send text for translation"));
                return;
            }

            await next();
        }
    }
}