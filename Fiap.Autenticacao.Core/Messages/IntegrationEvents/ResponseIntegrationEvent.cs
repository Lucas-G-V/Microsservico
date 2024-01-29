using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Autenticacao.Core.Messages.IntegrationEvents
{
    public class ResponseIntegrationEvent
    {
        public ValidationResult ValidationResult { get; set; }

        public ResponseIntegrationEvent(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
