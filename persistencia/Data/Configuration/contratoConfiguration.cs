
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class ContratoConfiguration : IEntityTypeConfiguration<Contrato> 
{
    public void Configure(EntityTypeBuilder<Contrato> builder)
    {

        builder.ToTable("contrato");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.FechaContrato)
        .HasColumnType("date")
        .IsRequired()
        .HasMaxLength(20);

        builder.Property(p => p.FechaFin)
        .HasColumnType("date")
        .IsRequired()
        .HasMaxLength(20);

        builder.HasOne(p => p.Cliente)
        .WithMany(p => p.Clientes)
        .HasForeignKey(p => p.IdClienteFk);

        builder.HasOne(p => p.Empleado)
        .WithMany(p => p.Empleados)
        .HasForeignKey(p => p.IdEmpleadoFk);

        builder.HasOne(p => p.Estado)
        .WithMany(p => p.Contratos)
        .HasForeignKey(p => p.IdEstadoFk);
    }
}
