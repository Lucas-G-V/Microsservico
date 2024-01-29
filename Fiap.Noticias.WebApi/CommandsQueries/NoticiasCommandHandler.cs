using Fiap.Autenticacao.Core.Messages;
using Fiap.Noticias.WebApi.CommandsQueries.ValidationClasses;
using Fiap.Noticias.WebApi.HttpServices;
using Fiap.Noticias.WebApi.Model;
using Fiap.Noticias.WebApi.Model.RepositoryInterfaces;
using FluentValidation.Results;
using MediatR;

namespace Fiap.Noticias.WebApi.CommandsQueries
{
    public class NoticiasCommandHandler : CommandHandler,
        IRequestHandler<CriarAutorCommand, ValidationResult>
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IEmailService _emailService;
        protected ValidationResult ValidationResult { get; set; }
        public NoticiasCommandHandler(IAutorRepository autorRepository, IEmailService emailService)
        {
            _autorRepository = autorRepository;
            _emailService = emailService;
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        public async Task<ValidationResult> Handle(CriarAutorCommand request, CancellationToken cancellationToken)
        {
            if(!request.ClasseEstaValida()) return request.RetornaValidacao();
            var novoAutor = new Autor(request.Nome, request.Email, request.DataNascimento);
            if(!novoAutor.EhValido()) return novoAutor.RetornaClasseDeValidacao();
            await _autorRepository.Add(novoAutor);
            await _emailService.EnviaEmail();
            return new ValidationResult();
        }
    }
}
