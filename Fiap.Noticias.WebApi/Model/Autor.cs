using FluentValidation;
using FluentValidation.Results;

namespace Fiap.Noticias.WebApi.Model
{
    public class Autor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } 
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        private ValidationResult ValidacaoDaClasse { get; set; }
        public Autor(string nome, string email, DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
        }

        public bool EhValido()
        {
            var errosEncontrados = new AutorValidation().Validate(this).Errors;
            ValidacaoDaClasse = new ValidationResult(errosEncontrados);
            return ValidacaoDaClasse.IsValid;
        }

        public ValidationResult RetornaClasseDeValidacao()
        {
            return ValidacaoDaClasse;
        }
    }

    public class AutorValidation : AbstractValidator<Autor>
    {
        public AutorValidation() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(255)
                .WithMessage("Nome deve ter entre 2 e 255 caracteres");
            RuleFor(c => c.Email)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(255)
                .EmailAddress()
                .WithMessage("Email deve ter entre 2 e 255 caracteres e ser um email válido");

            RuleFor(d => d.DataNascimento)
                .LessThan(DateTime.Now.AddYears(-18))
                .WithMessage("O Autor deve ser maior de idade");
        }

    }
}
