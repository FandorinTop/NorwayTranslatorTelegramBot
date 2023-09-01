using NorwayTranslatorTelegramBot.Entities;
using NorwayTranslatorTelegramBot.Translator.Azure.Languages;
using NorwayTranslatorTelegramBot.ViewModel.Translator;
using System.Net.Http.Json;

namespace NorwayTranslatorTelegramBot.Translator.Azure
{
    public class AzureTranslator : ITranslator
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<Language, IAzureToLanguageRequestFactory> AwailableLanguages = new Dictionary<Language, IAzureToLanguageRequestFactory>();

        public AzureTranslator(HttpClient httpClient, IEnumerable<IAzureToLanguageRequestFactory> awailableTrablators)
        {
            _httpClient = httpClient;

            foreach (var item in awailableTrablators)
            {
                if (AwailableLanguages.ContainsKey(item.ToLanguage))
                {
                    AwailableLanguages[item.ToLanguage] = item;
                }
                else
                {
                    AwailableLanguages.Add(item.ToLanguage, item);
                }
            }
        }

        public async Task<TranslationResult> TranslateAsync(string text, Language toLanguage)
        {
            if (!AwailableLanguages.ContainsKey(toLanguage))
                throw new NotImplementedException($"Language: ({toLanguage}) is not supported by current translator");
                //TODO REPLACE WITH OWN EXCEPTION
                //throw new NotImplementedException($"Language: ({toLanguage}) is not supported by current translator");

            var requestMessage = AwailableLanguages[toLanguage].Create(text);
            var translationResponce = await _httpClient.SendAsync(requestMessage);

            if (translationResponce.IsSuccessStatusCode)
            {
                var translationResult = await translationResponce.Content.ReadFromJsonAsync<List<TranslationResult>>();
                return translationResult?.First() ?? new TranslationResult();
            }

            //TODO REPLACE
            throw new Exception();
        }
    }
}