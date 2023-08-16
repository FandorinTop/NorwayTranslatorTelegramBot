using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace NorwayTranslatorTelegramBot.Translator.Azure.Languages.Base
{
    public class BaseAzureTranslateRequest
    {
        private const string SectionName = "AzureTranslation";

        protected readonly string Route = "";
        protected readonly string Endpoint = "";
        protected readonly string Key = "";
        protected readonly string Region = "";

        public BaseAzureTranslateRequest(IConfiguration configuration)
        {
            var speechSection = configuration.GetSection(SectionName)
                ?? throw new ArgumentNullException(nameof(SectionName)); ;
            Key = speechSection[nameof(Key)]
                ?? throw new ArgumentNullException($"${nameof(Key)} is null");
            Region = speechSection[nameof(Region)]
                ?? throw new ArgumentNullException($"${nameof(Region)} is null");
            Route = speechSection[nameof(Route)]
                ?? throw new ArgumentNullException($"${nameof(Route)} is null");
            Endpoint = speechSection[nameof(Endpoint)]
                ?? throw new ArgumentNullException($"${nameof(Endpoint)} is null");
        }

        public virtual HttpRequestMessage Create(string textToTranslate)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };
            Factory.AddSubscriptions(request, Key, Region);

            request.RequestUri = new Uri(Endpoint + Route);
            object[] body = new object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            return request;
        }

        public static class Factory
        {
            internal static void AddSubscriptions(HttpRequestMessage request, string key, string region)
            {
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                request.Headers.Add("Ocp-Apim-Subscription-Region", region);
            }

            public static HttpRequestMessage Create(
                string uri,
                string subscriptionKey,
                string subscriptionRegion,
                string textToTranslate,
                string toLanguage)
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(uri + $"&{toLanguage}")
                };
                AddSubscriptions(request, subscriptionKey, subscriptionRegion);

                object[] body = new object[] { new { Text = textToTranslate } };
                var requestBody = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                return request;
            }
        }
    }
}