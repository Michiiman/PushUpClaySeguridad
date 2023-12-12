
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class PaisConfiguration : IEntityTypeConfiguration<Pais> 
{
    public void Configure(EntityTypeBuilder<Pais> builder)
    {

        builder.ToTable("pais");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Nombre)
        .HasColumnName("Nombre")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

    }
}
