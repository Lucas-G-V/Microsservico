namespace Fiap.Noticias.WebApi.Model.RepositoryInterfaces
{
    public interface IAutorRepository
    {
        Task<int> Add(Autor autor);
        Task<int> Update(Autor autor);
        Task<Autor> GetByEmail(string email);
    }
}
