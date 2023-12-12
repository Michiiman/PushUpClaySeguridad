
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class TurnosConfiguration : IEntityTypeConfiguration<Turnos> 
{
    public void Configure(EntityTypeBuilder<Turnos> builder)
    {

        builder.ToTable("turnos");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.NombreTurno)
        .HasColumnName("NombreTurno")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.HoraTurnoInicial)
        .IsRequired()
        .HasMaxLength(20);

        builder.Property(p => p.HoraTurnoFinal)
        .IsRequired()
        .HasMaxLength(20);

    }
}
