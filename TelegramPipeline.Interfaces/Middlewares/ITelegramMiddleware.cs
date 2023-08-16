using TelegramPipeline.Responces;

namespace TelegramPipeline.Interfaces.Middlewares
{
    public interface ITelegramMiddleware
    {
        public Task InvokeAsync(TelegramContext context, Func<Task> next);
    }
}