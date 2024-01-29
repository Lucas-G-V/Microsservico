using Fiap.Autenticacao.Core.ConfiguracaoFila;
using Fiap.Autenticacao.Core.Messages.IntegrationEvents;
using Fiap.Autenticacao.Core.Messages.IntegrationEvents.Autenticacao_Noticias;
using Fiap.Autenticacao.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Autenticacao.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly IFilaMensagem _bus;
        public AutenticacaoController(UserManager<IdentityUser> userManager, IFilaMensagem filaMensagem)
        {
            UserManager = userManager;
            _bus = filaMensagem;
        }

        [HttpPost("novo-usuario")]
        public async Task<ActionResult> RegistarNovaConta(UsuarioNovaContaDto usuario)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var userIndentity = new IdentityUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true,
            };

            await UserManager.CreateAsync(userIndentity, usuario.Senha);
            var criaAutor = await CriarNovoAutor(usuario);
            if(!criaAutor) return BadRequest("Erro no retorno");
            return NoContent();
        }

        private async Task<bool> CriarNovoAutor(UsuarioNovaContaDto usuario)
        {
            var result = await _bus.RequestAsync<UsuarioCriadoIntegrationEvent, ResponseIntegrationEvent>(new UsuarioCriadoIntegrationEvent(
                usuario.Nome, usuario.Email, usuario.Senha, usuario.SenhaConfirmacao, usuario.DataNascimento));
            if (!result.ValidationResult.IsValid)
            {
                var usuarioBanco = await UserManager.FindByEmailAsync(usuario.Email);
                await UserManager.DeleteAsync(usuarioBanco);
                return false;
            }
            return true;
        }

        [HttpPut("alterar-usuario")]
        public async Task<ActionResult> AlterarNovaConta(UsuarioAlterarInformacoesDto usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userIndentity = new IdentityUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true,
            };

            await UserManager.ChangePasswordAsync(userIndentity, usuario.Senha, usuario.SenhaNova);
            await AlterarAutor(usuario);
            return NoContent();
        }

        private async Task AlterarAutor(UsuarioAlterarInformacoesDto usuario)
        {
            await _bus.PublishAsync<AlterarUsuarioIntegrationEvent>(new AlterarUsuarioIntegrationEvent(
                usuario.Nome, usuario.Email, usuario.DataNascimento));
        }
    }
}
