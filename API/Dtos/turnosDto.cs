using Dominio.Entities;
namespace API.Dtos;
public class TurnosDto : BaseEntity
{
    public string NombreTurno { get; set; }
    public string HoraTurnoInicial { get; set; }
    public string HoraTurnoFinal { get; set; }
}

