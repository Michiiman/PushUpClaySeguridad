
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class PersonaConfiguration : IEntityTypeConfiguration<Persona> 
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {

        builder.ToTable("persona");
        builder.HasAlternateKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Nombre)
        .HasColumnName("Nombre")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.FechaRegistro)
        .IsRequired()
        .HasColumnType("Date")
        .HasMaxLength(20);

        builder.HasOne(p => p.TipoPersona)
        .WithMany(p => p.Personas)
        .HasForeignKey(p => p.IdTipoPersona);

        builder.HasOne(p => p.CategoriaPer)
        .WithMany(p => p.Personas)
        .HasForeignKey(p => p.IdCategoriaPerFk);

        builder.HasOne(p => p.Ciudad)
        .WithMany(p => p.Personas)
        .HasForeignKey(p => p.IdCiudadFk);


    }
}
