using System.Collections;
using Telegram.Bot.Types;

namespace TelegramPipeline.Responces
{
    public class TelegramContext : IEnumerable<ITelegramResponceMessage>
    {
        public TelegramContext(Message request)
        {
            Request = request;
        }

        public Message Request { get; }

        private List<ITelegramResponceMessage> MessageToSends { get; } = new List<ITelegramResponceMessage>();

        public void Add(ITelegramResponceMessage responce)
        {
            MessageToSends.Add(responce);
        }

        public T? PeekLastOfType<T>() where T : class, ITelegramResponceMessage
        {
            var te = MessageToSends.FindLast(item => item.GetType() == typeof(T));

            return te as T;
        }

        public IEnumerator<ITelegramResponceMessage> GetEnumerator()
        {
            return MessageToSends.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}