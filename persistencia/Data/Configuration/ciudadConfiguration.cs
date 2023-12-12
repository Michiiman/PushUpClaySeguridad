using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad> 
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {

        builder.ToTable("ciudad");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.NombreCiudad)
        .HasColumnName("Nombre")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.HasOne(p => p.Departamento)
        .WithMany(p => p.Ciudades)
        .HasForeignKey(p => p.IdDepartamentoFk);

    }
}
