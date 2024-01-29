﻿using Fiap.Autenticacao.Core.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace Fiap.Noticias.WebApi.CommandsQueries.ValidationClasses
{
    public class AlterarAutorCommand : Command
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        private ValidationResult ValidationResult { get; set; }
        public DateTime DataAtual { get; set; }
        public AlterarAutorCommand(string nome, string email, DateTime dataNascimento)
        {
            DataAtual = DateTime.Now;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            ClasseEstaValida();
        }

        public bool ClasseEstaValida()
        {
            ValidationResult = new AlterarAutorCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public ValidationResult RetornaValidacao()
        {
            return ValidationResult;
        }
    }

    public class AlterarAutorCommandValidation : AbstractValidator<AlterarAutorCommand>
    {
        public AlterarAutorCommandValidation()
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
