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
        IRequestHandler<CriarAutorCommand, ValidationResult>,
        IRequestHandler<AlterarAutorCommand, ValidationResult>
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
            var validaEmail = await _autorRepository.GetByEmail(request.Email);
            if (validaEmail != null) 
            {
                return new ValidationResult(new List<ValidationFailure>
                    {
                        new ValidationFailure("Email", "Este email já está cadastrado.")
                    });
            }
            await _autorRepository.Add(novoAutor);
            await _emailService.EnviaEmail();
            return new ValidationResult();
        }

        public async Task<ValidationResult> Handle(AlterarAutorCommand request, CancellationToken cancellationToken)
        {
            if (!request.ClasseEstaValida()) return request.RetornaValidacao();
            var alterarAutor = new Autor(request.Nome, request.Email, request.DataNascimento);
            if (!alterarAutor.EhValido()) return alterarAutor.RetornaClasseDeValidacao();
            var autor = await _autorRepository.GetByEmail(request.Email);
            autor.DataNascimento = request.DataNascimento;
            await _autorRepository.Update(autor);
            return new ValidationResult();
        }
    }
}
