
using Dominio.Entities;
namespace API.Dtos;
public class ContactoPerDto : BaseEntity
{
    public string Descripcion { get; set; }
    public int IdPersonaFk { get; set; }
    public Persona Persona { get; set; }
    public int IdTipoContacto { get; set; }
    public TipoContacto TipoContacto { get; set; }
}
