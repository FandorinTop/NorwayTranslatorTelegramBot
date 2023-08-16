using System.Text.RegularExpressions;
using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Responces;

namespace TelegramPipeline.Middlewares.Filter
{
    public class EmojiFilterMiddleware : ITelegramMiddleware
    {
        public bool IsAEmoji(string text)
        {
            string pattern = @"(\u00a9|\u00ae|[\u2000-\u3300]|\ud83c[\ud000-\udfff]|\ud83d[\ud000-\udfff]|\ud83e[\ud000-\udfff])";
            return Regex.IsMatch(text, pattern);
        }

        public async Task InvokeAsync(TelegramContext context, Func<Task> next)
        {
            Console.WriteLine($"Start {nameof(EmojiFilterMiddleware)}");
            if (IsAEmoji(context.Request.Text!))
            {
                context.Add(new TelegramErrorResponceMessage($"Remove emoji from text"));
                Console.WriteLine($"Filter stop {nameof(EmojiFilterMiddleware)} for text: '{context.Request.Text!}' ");
                return;
            }

            Console.WriteLine($"Passed {nameof(EmojiFilterMiddleware)}");

            await next();
        }
    }
}