
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class ContactoPerConfiguration : IEntityTypeConfiguration<ContactoPer> 
{
    public void Configure(EntityTypeBuilder<ContactoPer> builder)
    {

        builder.ToTable("contactoPer");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.HasAlternateKey(c => c.Descripcion);
        
        builder.Property(p => p.Descripcion)
        .HasColumnName("Descripcion")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();
        
        builder.HasOne(p => p.TipoContacto)
        .WithMany(p => p.ContactoPersonas)
        .HasForeignKey(p => p.IdTipoContacto);

        builder.HasOne(p => p.Persona)
        .WithMany(p => p.ContactoPersonas)
        .HasForeignKey(p => p.IdPersonaFk);

    }
}
