using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class DirPersonaConfiguration : IEntityTypeConfiguration<DirPersona> 
{
    public void Configure(EntityTypeBuilder<DirPersona> builder)
    {

        builder.ToTable("dirPersona");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Direccion)
        .HasColumnName("Direccion")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.HasOne(p => p.TipoDireccion)
        .WithMany(p => p.DirPersonas)
        .HasForeignKey(p => p.IdTipoDireccionFk);

        builder.HasOne(p => p.Persona)
        .WithMany(p => p.DirPersonas)
        .HasForeignKey(p => p.IdPersonaFk);

    }
}
