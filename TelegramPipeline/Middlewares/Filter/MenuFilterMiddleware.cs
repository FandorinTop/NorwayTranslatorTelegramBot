using System.Text.RegularExpressions;
using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Responces;

namespace TelegramPipeline.Middlewares.Filter
{
    public class MenuFilterMiddleware : ITelegramMiddleware
    {
        public bool IsAMenu(string text)
        {
            string pattern = @"^\s*[\\\/;.%!@#$*()_+=]";
            return Regex.IsMatch(text, pattern);
        }

        public async Task InvokeAsync(TelegramContext context, Func<Task> next)
        {
            if (string.IsNullOrEmpty(context.Request.Text))
            {
                context.Add(new TelegramErrorResponceMessage($"Send text for translation"));
                return;
            }
            else if (IsAMenu(context.Request.Text))
            {
                Console.WriteLine("Menu request");
                return;
            }

            await next();
        }
    }
}