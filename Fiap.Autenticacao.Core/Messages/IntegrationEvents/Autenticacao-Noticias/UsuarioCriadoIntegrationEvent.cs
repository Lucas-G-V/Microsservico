using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Autenticacao.Core.Messages.IntegrationEvents.Autenticacao_Noticias
{
    public class UsuarioCriadoIntegrationEvent : IntegrationEvent
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string SenhaConfirmacao { get; set; }
        public DateTime DataNascimento { get; set; }

        public UsuarioCriadoIntegrationEvent(string nome, string email, string senha, string senhaConfirmacao, DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            SenhaConfirmacao = senhaConfirmacao;
            DataNascimento = dataNascimento;
        }
    }
}
