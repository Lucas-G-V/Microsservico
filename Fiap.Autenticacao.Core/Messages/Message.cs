using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Autenticacao.Core.Messages
{
    public abstract class Message
    {
        public string TipoMensagem { get; protected set; }
        public Guid Id { get; protected set; }

        protected Message()
        {
            TipoMensagem = GetType().Name;
        }
    }
}
