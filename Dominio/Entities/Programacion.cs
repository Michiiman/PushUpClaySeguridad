namespace Dominio.Entities;
public class Programacion : BaseEntity
{
    public int IdContratoFk {get;set;}
    public Contrato Contrato {get;set;}
    public int IdTurnosFk {get;set;}
    public Turnos Turno {get;set;}
    public int IdEmpleadoFk {get;set;}
    public Persona Empleado {get;set;}
    
}
