namespace TelegramPipeline.Pipelines
{
    public class GeneralChain<TElement>
    {
        protected GeneralChain<TElement> next = default!;

        public GeneralChain<TElement> SetNext(TElement telegramMiddleware)
        {
            next = new GeneralChain<TElement>();
            next.SetNext(telegramMiddleware);

            return next;
        }

        public virtual async Task ExecuteAsync()
        {
            if (next is not null)
            {
                await next.ExecuteAsync();
            }
        }
    }
}