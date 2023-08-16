using Microsoft.Extensions.Configuration;
using NorwayTranslatorTelegramBot.Entities;
using NorwayTranslatorTelegramBot.Translator.Azure.Languages.Base;

namespace NorwayTranslatorTelegramBot.Translator.Azure.Languages
{
    public class AzureToRussianRequestFactory : BaseAzureTranslateRequest, IAzureToLanguageRequestFactory
    {
        private const string countryName = "ru";
        private readonly string countryQuery = $"&to={countryName}";

        public AzureToRussianRequestFactory(IConfiguration configuration) : base(configuration)
        {
        }

        public Language ToLanguage => Language.Russian;

        public override HttpRequestMessage Create(string textToTranslate)
        {
            var baseRequest = base.Create(textToTranslate);
            baseRequest.RequestUri = new Uri(baseRequest.RequestUri + countryQuery);

            return baseRequest;
        }
    }
}