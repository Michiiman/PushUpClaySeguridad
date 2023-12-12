using Dominio.Entities;
namespace API.Dtos;
public class PersonaDto : BaseEntity
{
    public string Nombre { get; set; }
    public DateOnly FechaRegistro { get; set; }
    public int IdTipoPersona { get; set; }
    public TipoPersona TipoPersona { get; set; }
    public int IdCategoriaPerFk { get; set; }
    public CategoriaPer CategoriaPer { get; set; }
    public int IdCiudadFk { get; set; }
    public Ciudad Ciudad { get; set; }
}

