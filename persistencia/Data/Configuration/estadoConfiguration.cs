
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class EstadoConfiguration : IEntityTypeConfiguration<Estado> 
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {

        builder.ToTable("estado");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Descripcion)
        .HasColumnName("descripcion")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();


    }
}
