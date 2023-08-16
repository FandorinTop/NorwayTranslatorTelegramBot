using Microsoft.Extensions.Configuration;
using NorwayTranslatorTelegramBot.Entities;
using NorwayTranslatorTelegramBot.Translator.Azure.Languages.Base;

namespace NorwayTranslatorTelegramBot.Translator.Azure.Languages
{
    public class AzureToNorwayRequestFactory : BaseAzureTranslateRequest, IAzureToLanguageRequestFactory
    {
        private const string countryName = "nb";
        private readonly string countryQuery = $"&to={countryName}";

        public AzureToNorwayRequestFactory(IConfiguration configuration) : base(configuration)
        {
        }

        public Language ToLanguage => Language.Norwegian;

        public override HttpRequestMessage Create(string textToTranslate)
        {
            var baseRequest = base.Create(textToTranslate);
            baseRequest.RequestUri = new Uri(baseRequest.RequestUri + countryQuery);

            return baseRequest;
        }
    }
}