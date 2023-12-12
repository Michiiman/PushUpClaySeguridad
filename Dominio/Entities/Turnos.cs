namespace Dominio.Entities;
public class Turnos : BaseEntity
{
    public string NombreTurno { get; set; }
    public string HoraTurnoInicial { get; set; }
    public string HoraTurnoFinal { get; set; }

    public ICollection<Programacion> Programaciones { get; set; }
}

