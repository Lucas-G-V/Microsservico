using EasyNetQ;
using Fiap.Autenticacao.Core.Messages.IntegrationEvents;


namespace Fiap.Autenticacao.Core.ConfiguracaoFila
{
    public class FilaMensagem : IFilaMensagem
    {
        private IBus _bus;
        private bool EstaConectado = false;
        public FilaMensagem()
        {
            ConectaSeNaoEstiver();
        }
        public void Publish<T>(T message) where T : IntegrationEvent
        {
            _bus.PubSub.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            ConectaSeNaoEstiver();
            await _bus.PubSub.PublishAsync(message);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseIntegrationEvent
        {
            ConectaSeNaoEstiver();
            return _bus.Rpc.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseIntegrationEvent
        {
            ConectaSeNaoEstiver();
            return await _bus.Rpc.RequestAsync<TRequest, TResponse>(request);
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseIntegrationEvent
        {
            ConectaSeNaoEstiver();
            return _bus.Rpc.Respond(responder);
        }

        public async Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseIntegrationEvent
        {
            ConectaSeNaoEstiver();
            return await _bus.Rpc.RespondAsync(responder);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            ConectaSeNaoEstiver();
            _bus.PubSub.Subscribe(subscriptionId, onMessage);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            ConectaSeNaoEstiver();
            _bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }

        public void ConectaSeNaoEstiver()
        {
            if(!EstaConectado) _bus = RabbitHutch.CreateBus("host=localhost");
            EstaConectado = true;
        }
    }
}
