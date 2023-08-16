using Microsoft.Extensions.Configuration;
using NorwayTranslatorTelegramBot.Entities;
using NorwayTranslatorTelegramBot.Translator.Azure.Languages.Base;

namespace NorwayTranslatorTelegramBot.Translator.Azure.Languages
{
    public class AzureToEnglishRequestFactory : BaseAzureTranslateRequest, IAzureToLanguageRequestFactory
    {
        private const string countryName = "en";
        private readonly string countryQuery = $"&to={countryName}";

        public AzureToEnglishRequestFactory(IConfiguration configuration) : base(configuration)
        {
        }

        public Language ToLanguage => Language.English;

        public override HttpRequestMessage Create(string textToTranslate)
        {
            var baseRequest = base.Create(textToTranslate);
            baseRequest.RequestUri = new Uri(baseRequest.RequestUri + countryQuery);

            return baseRequest;
        }
    }
}