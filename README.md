# Projeto de Integração de APIs com RabbitMQ

Este projeto propõe criar uma API principal para a criação de usuários, que se comunica com APIs secundárias usando RabbitMQ para garantir a integridade e escalabilidade do sistema. A API principal recebe solicitações para criar usuários e as envia para APIs secundárias usando filas RabbitMQ.

## Arquitetura

A arquitetura do sistema é baseada em microserviços e utiliza o RabbitMQ como o serviço de mensageria. A comunicação entre a API principal e as APIs secundárias é realizada através de mensagens RabbitMQ, utilizando EasyNetQ como uma biblioteca de cliente RabbitMQ para simplificar o processo de envio e recebimento de mensagens.

## Funcionalidades Principais

- **Criação de Usuários:** A API principal expõe um endpoint para receber solicitações de criação de usuários.
- **Integração com APIs Secundárias:** As solicitações recebidas pela API principal são enviadas para APIs secundárias usando RabbitMQ.
- **Padrões de Mensageria:** O sistema utiliza os padrões de publish/subscribe e request/response do RabbitMQ para garantir a entrega e resposta das mensagens entre os componentes do sistema.

## Configuração do Projeto

Para executar o projeto localmente, siga estas etapas:

1. Clone o repositório: `git clone https://github.com/seu-usuario/nome-do-repositorio.git`
2. Instale as dependências do projeto: `dotnet restore`
3. Configure as variáveis de ambiente necessárias, incluindo as credenciais do RabbitMQ.
4. Execute a aplicação: `dotnet run`

Certifique-se de ter um servidor RabbitMQ em execução e configurado corretamente para que o sistema funcione corretamente.

## Licença

Este projeto está licenciado sob a [Licença MIT](https://github.com/seu-usuario/nome-do-repositorio/blob/main/LICENSE).


