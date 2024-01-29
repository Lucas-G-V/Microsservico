
using Fiap.Autenticacao.Core.ConfiguracaoFila;
using Fiap.Autenticacao.Core.Mediatr;
using Fiap.Autenticacao.Core.Messages.IntegrationEvents;
using Fiap.Autenticacao.Core.Messages.IntegrationEvents.Autenticacao_Noticias;
using Fiap.Noticias.WebApi.CommandsQueries.ValidationClasses;
using FluentValidation.Results;
using MediatR;

namespace Fiap.Noticias.WebApi.Services
{
    public class CriarAutorFilaHandler : BackgroundService
    {
        private readonly IFilaMensagem _bus;
        private readonly IServiceProvider _serviceProvider;
        public CriarAutorFilaHandler(IFilaMensagem bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.RespondAsync<UsuarioCriadoIntegrationEvent, ResponseIntegrationEvent>(async request =>
            await CriarAutor(request));

            return Task.CompletedTask;
        }

        private async Task<ResponseIntegrationEvent> CriarAutor(UsuarioCriadoIntegrationEvent autor)
        {
            var clienteCommand = new CriarAutorCommand(autor.Nome, autor.Email, autor.DataNascimento);
            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatrHandler>();
                sucesso = await mediator.EnviarNovoComando(clienteCommand);
            }

            return new ResponseIntegrationEvent(sucesso);
        }
    }
}
