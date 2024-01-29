using Fiap.Noticias.WebApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Noticias.WebApi.Data.Mappings
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.DataNascimento)
                .IsRequired();

            builder.ToTable("Autores");
        }
    }
}
