
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class CategoriaPerConfiguration : IEntityTypeConfiguration<CategoriaPer> 
{
    public void Configure(EntityTypeBuilder<CategoriaPer> builder)
    {

        builder.ToTable("categoriaPer");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.NombreCat)
        .HasColumnName("nombreCat")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();
        

    }
}
