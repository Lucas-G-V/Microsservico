using Fiap.Noticias.WebApi.Data.Context;
using Fiap.Noticias.WebApi.Model;
using Fiap.Noticias.WebApi.Model.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Noticias.WebApi.Data.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly NoticiaDbContext _db;
        private readonly DbSet<Autor> _dbSet;

        public AutorRepository(NoticiaDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<Autor>();
        }
        public async Task<int> Add(Autor autor)
        {
            _dbSet.Add(autor);
            return await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
