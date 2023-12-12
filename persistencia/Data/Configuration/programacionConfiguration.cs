
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class ProgramacionConfiguration : IEntityTypeConfiguration<Programacion> 
{
    public void Configure(EntityTypeBuilder<Programacion> builder)
    {

        builder.ToTable("programacion");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.HasOne(p => p.Contrato)
        .WithMany(p => p.Programaciones)
        .HasForeignKey(p => p.IdContratoFk);

        builder.HasOne(p => p.Turno)
        .WithMany(p => p.Programaciones)
        .HasForeignKey(p => p.IdTurnosFk);

        builder.HasOne(p => p.Empleado)
        .WithMany(p => p.Programaciones)
        .HasForeignKey(p => p.IdEmpleadoFk);
    }
}
