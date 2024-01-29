using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Autenticacao.Core.Messages
{
    public class Event : Message, INotification
    {
        public DateTime DataAtual { get; private set; }

        protected Event()
        {
            DataAtual = DateTime.Now;
        }
    }
}
