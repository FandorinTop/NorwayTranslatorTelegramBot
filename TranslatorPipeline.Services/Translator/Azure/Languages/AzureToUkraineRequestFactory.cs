using Microsoft.Extensions.Configuration;
using NorwayTranslatorTelegramBot.Entities;
using NorwayTranslatorTelegramBot.Translator.Azure.Languages.Base;

namespace NorwayTranslatorTelegramBot.Translator.Azure.Languages
{
    public class AzureToUkraineRequestFactory : BaseAzureTranslateRequest, IAzureToLanguageRequestFactory
    {
        private const string countryName = "uk";
        private readonly string countryQuery = $"&to={countryName}";

        public AzureToUkraineRequestFactory(IConfiguration configuration) : base(configuration)
        {
        }

        public Language ToLanguage => Language.Ukraine;

        public override HttpRequestMessage Create(string textToTranslate)
        {
            var baseRequest = base.Create(textToTranslate);
            baseRequest.RequestUri = new Uri(baseRequest.RequestUri + countryQuery);

            return baseRequest;
        }
    }
}