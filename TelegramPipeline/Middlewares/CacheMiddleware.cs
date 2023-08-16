using CacheDomainContext;
using CacheEntities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Responces;

namespace TelegramPipeline.Middlewares
{
    public class CacheMiddleware : ITelegramMiddleware
    {
        private readonly CacheDbContext dbContext;

        public CacheMiddleware(CacheDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task InvokeAsync(TelegramContext context, Func<Task> next)
        {
            try
            {
                Console.WriteLine("Start Cache Middleware");
                Console.WriteLine("Searching cache");
                var cache = await dbContext.CachedMessages
                    .FirstOrDefaultAsync(item => item.Text.Equals(context.Request.Text));

                if (cache != null)
                {
                    var cahcedMessages = GetCached(cache);

                    foreach (var message in cahcedMessages)
                    {
                        context.Add(message);
                    }

                    Console.WriteLine("Cache hit, success");
                    return;
                }

                Console.WriteLine("No cache");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await next();
        }

        private IEnumerable<ITelegramResponceMessage> GetCached(CacheTgMessage cache)
        {
            if (!string.IsNullOrEmpty(cache.TranslatedText))
                yield return new TelegramTranslateResponceMessage(cache.TranslatedText);

            if (!string.IsNullOrEmpty(cache.TelegramFileId))
                yield return new TelegramAudioResponceMessage(new InputFileId(cache.TelegramFileId));
        }
    }
}