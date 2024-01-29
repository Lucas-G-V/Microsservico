using System.Net.Http;

namespace Fiap.Noticias.WebApi.HttpServices
{
    public interface IEmailService
    {
        Task EnviaEmail();
    }
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;

        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task EnviaEmail()
        {
            await _httpClient.GetAsync("http://localhost:7169/api/Email");
        }
    }
}
