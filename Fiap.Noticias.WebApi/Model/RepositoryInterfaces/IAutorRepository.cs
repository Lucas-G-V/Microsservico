namespace Fiap.Noticias.WebApi.Model.RepositoryInterfaces
{
    public interface IAutorRepository
    {
        Task<int> Add(Autor autor);
    }
}
