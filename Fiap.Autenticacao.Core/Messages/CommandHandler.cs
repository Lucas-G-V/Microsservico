using FluentValidation.Results;

namespace Fiap.Autenticacao.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult Validacao { get; set; }

        protected CommandHandler()
        {
            Validacao = new ValidationResult();
        }

        protected void AdicionarErro(string mensagem)
        {
            Validacao.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }
    }
}
