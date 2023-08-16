using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Responces;

namespace TelegramPipeline.Middlewares
{
    public class ExecutionTimeMiddleware : ITelegramMiddleware
    {
        public ExecutionTimeMiddleware()
        {
        }

        public async Task InvokeAsync(TelegramContext context, Func<Task> next)
        {
            Console.WriteLine($"Start (UTC): {DateTime.UtcNow}");
            await next();
            Console.WriteLine($"End (UTC): {DateTime.UtcNow}");
        }
    }
}