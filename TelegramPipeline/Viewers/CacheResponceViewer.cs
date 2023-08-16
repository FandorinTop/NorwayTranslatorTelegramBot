using CacheDomainContext;
using CacheEntities;
using Microsoft.EntityFrameworkCore;
using NorwayTranslatorTelegramBot.Entities;
using Telegram.Bot.Types;
using TelegramPipeline.Responces;

namespace TelegramPipeline.Viewers
{
    public class CacheResponceViewer : ITelegramSendedMessageViewer
    {
        private readonly CacheDbContext context;

        public CacheResponceViewer(CacheDbContext context)
        {
            this.context = context;
        }

        public async Task ViewResultAsync(Message request, IReadOnlyDictionary<ITelegramResponceMessage, Message> dictionary)
        {
            var hasError = dictionary.Any(item => item.Key is TelegramErrorResponceMessage);

            if (hasError)
                return;

            var cache = await context.CachedMessages
                .FirstOrDefaultAsync(item => item.Text == request.Text);

            if (cache is null)
            {
                try
                {
                    cache = CreateCache(request, dictionary);
                    context.CachedMessages.Add(cache);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return;
        }

        //TODO ADD MAPPER
        private static CacheTgMessage CreateCache(Message request, IReadOnlyDictionary<ITelegramResponceMessage, Message> dictionary)
        {
            var cache = new CacheTgMessage
            {
                Text = request.Text!,
                ToLanguage = Language.Norwegian
            };

            if (dictionary.Keys.LastOrDefault(item => item is TelegramTranslateResponceMessage) is TelegramTranslateResponceMessage translation)
            {
                cache.TranslatedText = translation.Text;
            }

            var audio = dictionary.Keys.LastOrDefault(item => item is TelegramAudioResponceMessage);
            if (audio != null)
            {
                cache.TelegramFileId = dictionary[audio].Audio!.FileId;
            }

            return cache;
        }
    }
}
