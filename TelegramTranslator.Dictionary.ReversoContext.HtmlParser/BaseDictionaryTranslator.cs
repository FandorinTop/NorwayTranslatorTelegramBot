namespace TelegramTranslator.Dictionary.ReversoContext.HtmlParser
{
    public class BaseDictionaryTranslator
    {
        protected string RequestUrl { get; set; } = @"https://context.reverso.net/translation";

        public virtual string CreateTranslationPath(string value)
        {
            return string.Empty;
        }
    }
}
