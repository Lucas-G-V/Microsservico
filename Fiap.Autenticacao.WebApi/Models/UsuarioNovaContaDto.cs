﻿using System.ComponentModel.DataAnnotations;

namespace Fiap.Autenticacao.WebApi.Models
{
    public class UsuarioNovaContaDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} não possui formato de email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas de confirmação precisam ser iguais.")]
        public string SenhaConfirmacao { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
