using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorwayTranslatorTelegramBot.TextToSpeech;
using NorwayTranslatorTelegramBot.TextToSpeech.Azure;
using NorwayTranslatorTelegramBot.Translator;
using NorwayTranslatorTelegramBot.Translator.Azure;
using NorwayTranslatorTelegramBot.Translator.Azure.Languages;
using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Middlewares;
using TelegramPipeline.Middlewares.Filter;
using TelegramPipeline.Pipelines;
using TelegramPipeline.Viewers;

namespace TelegramPipeline
{
    public static class ServiceConfiguration
    {
        public static void AddDefaultServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClient>();
            services.AddTransient<NullOrEmptyFilterMiddleware>();
            services.AddTransient<MaxLengthFilterMiddleware>();
            services.AddTransient<EmojiFilterMiddleware>();
            services.AddTransient<MenuFilterMiddleware>();
            services.AddTransient<CacheMiddleware>();
            services.AddTransient<TranslateResponceMiddleware>();
            services.AddTransient<VoiceResponceMiddleware>();
        }

        public static void AddAzureTranslator(this IServiceCollection services)
        {
            services.AddSingleton<IAzureToLanguageRequestFactory, AzureToNorwayRequestFactory>();
            services.AddSingleton<IAzureToLanguageRequestFactory, AzureToEnglishRequestFactory>();
            services.AddSingleton<IAzureToLanguageRequestFactory, AzureToRussianRequestFactory>();
            services.AddSingleton<IAzureToLanguageRequestFactory, AzureToUkraineRequestFactory>();
            services.AddTransient<ITranslator, AzureTranslator>();
        }

        public static void AddAzureSynthezator(this IServiceCollection services)
        {
            services.AddSingleton<IAzureSpeechSetting, AzureNorwaySpeechSetting>();
            services.AddTransient<ITextToSpeechService, AzureTextToSpeechService>();
        }

        public static void AddMessageViewers(this IServiceCollection services)
        {
            services.AddTransient<ITelegramSendedMessageViewer, CacheResponceViewer>();
        }

        public static void AddDefaultMiddleware(this TelegramMessagePipeline pipeline)
        {
            pipeline.AddMiddleware<NullOrEmptyFilterMiddleware>();
            pipeline.AddMiddleware<MaxLengthFilterMiddleware>();
            pipeline.AddMiddleware<EmojiFilterMiddleware>();
            pipeline.AddMiddleware<MenuFilterMiddleware>();
            pipeline.AddMiddleware<CacheMiddleware>();
            pipeline.AddMiddleware<TranslateResponceMiddleware>();
            pipeline.AddMiddleware<VoiceResponceMiddleware>();
        }

        public static IConfigurationBuilder GetConfigurationBuilder()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return configBuilder;
        }
    }
}