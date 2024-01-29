using Fiap.Autenticacao.Core.Messages;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Autenticacao.Core.Mediatr
{
    public interface IMediatrHandler
    {
        Task PublicarNovoEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarNovoComando<T>(T comando) where T : Command;
    }

    public class MediatorHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ValidationResult> EnviarNovoComando<T>(T comando) where T : Command
        {
            return await _mediator.Send(comando);
        }

        public async Task PublicarNovoEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}
