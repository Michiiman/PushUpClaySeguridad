
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class TipoContactoConfiguration : IEntityTypeConfiguration<TipoContacto> 
{
    public void Configure(EntityTypeBuilder<TipoContacto> builder)
    {

        builder.ToTable("tipoContacto");
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
