
namespace Dominio.Entities;
public class Persona : BaseEntity
{
    public string Nombre { get; set; }
    public DateOnly FechaRegistro { get; set; }
    public int IdTipoPersona { get; set; }
    public TipoPersona TipoPersona { get; set; }
    public int IdCategoriaPerFk { get; set; }
    public CategoriaPer CategoriaPer { get; set; }
    public int IdCiudadFk { get; set; }
    public Ciudad Ciudad { get; set; }
    public ICollection<DirPersona> DirPersonas { get; set; }
    public ICollection<Contrato> Empleados { get; set; }
    public ICollection<Contrato> Clientes { get; set; }
    public ICollection<ContactoPer> ContactoPersonas { get; set; }
    public ICollection<Programacion> Programaciones { get; set; }

}
