using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Autenticacao.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime DataAtual { get; private set; }
        public ValidationResult Validacao { get; set; }

        protected Command()
        {
            DataAtual = DateTime.Now;
        }
    }
}
