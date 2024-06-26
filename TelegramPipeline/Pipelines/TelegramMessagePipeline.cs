﻿using Microsoft.Extensions.DependencyInjection;
using TelegramPipeline.Interfaces.Middlewares;
using TelegramPipeline.Middlewares;
using TelegramPipeline.Responces;
using TelegramTranslator.Common;

namespace TelegramPipeline.Pipelines
{
    public class TelegramMessagePipeline
    {
        private class DelegaTypeMessageHandler { }
        private IServiceProvider _serviceProvider = default!;
        private readonly IServiceCollection _serviceCollection;
        private readonly List<(Type, Func<TelegramContext, Func<Task>, Task>)> Handlers = new();

        public TelegramMessagePipeline(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void AddMiddleware(Func<TelegramContext, Func<Task>, Task> func)
        {
            Handlers.Add(new(typeof(DelegaTypeMessageHandler), func));
        }

        public void AddMiddleware<T>() where T : ITelegramMiddleware
        {
            Handlers.Add(new(typeof(T), (context, next) => Task.CompletedTask));
        }

        public async Task ProcessMessageAsync(TelegramContext telegramContext, CancellationToken cancellationToken = default!)
        {
            var root = new Chain(new ExecutionTimeMiddleware());
            var current = root;
            _serviceProvider = _serviceCollection.BuildServiceProvider();

            foreach (var handler in Handlers)
            {
                if (handler.Item1 == typeof(DelegaTypeMessageHandler))
                {
                    current = current.SetNext(handler.Item2);
                }
                else
                {
                    var service = _serviceProvider.GetService(handler.Item1) as ITelegramMiddleware;

                    if (service is null)
                    {
                        throw new TGArgumentNullException($"Service not registred {handler.Item1}");
                    }

                    current = current.SetNext(service);
                }
            }

            if (cancellationToken.IsCancellationRequested)
                return;

            await root.ExecuteAsync(telegramContext);
        }
    }
}