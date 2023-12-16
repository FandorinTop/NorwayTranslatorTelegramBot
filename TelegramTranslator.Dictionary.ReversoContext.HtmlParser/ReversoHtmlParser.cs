using System.Net.Http;
using TelegramTranslator.Dictionary.Interfaces;
using TelegramTranslator.Dictionary.Models;
using HtmlAgilityPack;
using NorwayTranslatorTelegramBot.Entities;
using TelegramTranslator.Dictionary.ReversoContext.HtmlParser.Interfaces;

namespace TelegramTranslator.Dictionary.ReversoContext.HtmlParser
{
    public class ReversoHtmlParser : IDictionaryRequestFactory
    {
        private HttpClient HttpClient { get; set; }
        private IDictionaryTranslatorFactory  DictionaryTranslatorFactory { get; set; }

        public async Task<DictionaryResponce> GetDictionaryResponce(string value, Language from, Language to)
        {
            var request = await HttpClient
                .GetAsync(DictionaryTranslatorFactory
                .GetTranslator(from, to)
                .CreateTranslationPath(value)
                );

            if (!request.IsSuccessStatusCode)
                throw new Exception("Translation not available");

            var html = await request.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var responce = MapResponce(doc);
            responce.From = from;
            responce.To = to;

            return responce;
        }

        private DictionaryResponce MapResponce(HtmlDocument doc)
        {
            var result = new DictionaryResponce();
            var suggestion = doc.GetElementbyId("dym-content");
            var translations = doc.GetElementbyId("translations-content");

            result.Suggestion = MapSuggestion(suggestion);
            result.Results = MapPartOfSpeech(translations);

            return result;
        }

        private IReadOnlyList<PartOfSpeechResponce> MapPartOfSpeech(HtmlNode transtations)
        {
            return default!;
        }

        public IReadOnlyList<string> MapSuggestion(HtmlNode suggestion)
        {
            return default!;
        }
    }
}
