using Fiap.Noticias.WebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Noticias.WebApi.Data.Context
{
    public class NoticiaDbContext : DbContext
    {
        public NoticiaDbContext(DbContextOptions<NoticiaDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Autor> Autores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(255)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NoticiaDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
