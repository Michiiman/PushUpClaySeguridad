
using Dominio.Entities;
namespace API.Dtos;
public class ContratoDto : BaseEntity
{
    public int IdClienteFk { get; set; }
    public Persona Cliente { get; set; }
    public DateOnly FechaContrato { get; set; }
    public int IdEmpleadoFk { get; set; }
    public Persona Empleado { get; set; }
    public DateOnly FechaFin { get; set; }
    public int IdEstadoFk { get; set; }
    public Estado Estado { get; set; }
}
