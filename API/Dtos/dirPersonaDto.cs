using Dominio.Entities;
namespace API.Dtos;
public class DirPersonaDto : BaseEntity
{
    public string Direccion { get; set; }
    public int IdPersonaFk { get; set; }
    public Persona Persona { get; set; }
    public int IdTipoDireccionFk { get; set; }
    public TipoDireccion TipoDireccion { get; set; }
}

