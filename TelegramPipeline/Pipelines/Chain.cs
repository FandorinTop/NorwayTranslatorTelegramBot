using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Responces;

namespace TelegramPipeline.Pipelines
{

    public class Chain
    {
        private readonly Func<TelegramContext, Func<Task>, Task> ProcessAsync;
        Chain next = default!;

        public Chain(Func<TelegramContext, Func<Task>, Task> deleg1)
        {
            ProcessAsync = deleg1;
        }

        public Chain(ITelegramMiddleware telegramMiddleware)
        {
            ProcessAsync = telegramMiddleware.InvokeAsync;
        }

        public Chain SetNext(ITelegramMiddleware telegramMiddleware)
        {
            next = new Chain(telegramMiddleware);

            return next;
        }

        public Chain SetNext(Func<TelegramContext, Func<Task>, Task> deleg1)
        {
            next = new Chain(deleg1);

            return next;
        }

        public async Task ExecuteAsync(TelegramContext context)
        {
            if (next is not null)
            {
                await ProcessAsync(context, () => next.ExecuteAsync(context));
            }
            else
            {
                await ProcessAsync(context, () => Task.CompletedTask);
            }
        }
    }
}