using CacheDomainContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramPipeline.Pipelines;
using TelegramPipeline.Viewers;
using TelegramTranslator.Common;
using static TelegramPipeline.ServiceConfiguration;

namespace TelegramPipeline
{
    internal partial class Program
    {
        static async Task Main()
        {
            var configurationBuilder = GetConfigurationBuilder();
            var Configuration = configurationBuilder.Build();
            var services = new ServiceCollection();

            services.AddDefaultServices();
            services.AddAzureSynthezator();
            services.AddAzureTranslator();
            services.AddMessageViewers();

            services.AddSingleton<IConfiguration>(item => Configuration);
            services.AddDbContext<CacheDbContext>(config => config.UseSqlServer(Configuration.GetConnectionString("DbConnectionString")));

            var pipeline = new TelegramMessagePipeline(services);

            pipeline.AddDefaultMiddleware();
            pipeline.AddMiddleware(async (context, next) =>
            {
                Console.WriteLine("Start TEST1 Middleware");
                await next();
                Console.WriteLine("End TEST1 Middleware");
            });

            await RunServerAsync(services, pipeline);
        }

        public static async Task RunServerAsync(IServiceCollection collection, TelegramMessagePipeline pipeline)
        {
            var provider = collection.BuildServiceProvider();

            const string TelegramSection = "Telegram";
            const string Token = "TranslationBotToken";
            var config = provider.GetService<IConfiguration>()!;
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var botClient = new TelegramBotClient(config.GetSection(TelegramSection)[Token]!)
                ?? throw new TGArgumentNullException($"No in section: '{TelegramSection}', key: '{Token}'");
            var opts = new ReceiverOptions();
            var resultViewers = provider.GetServices<ITelegramSendedMessageViewer>();
            var task = botClient.ReceiveAsync(new MessageHandler(pipeline, resultViewers), opts, cancellationToken);

            while (true)
            {
                Console.WriteLine("Press 'ctrl + d' to stop server");
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.D && key.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    cts.Cancel();
                    break;
                }
            }

            Console.WriteLine("stopping");
            await task;
            Console.WriteLine("stopped");
        }
    }
}