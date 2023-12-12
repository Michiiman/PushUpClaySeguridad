
namespace Dominio.Entities;
public class CategoriaPer : BaseEntity
{
    public string NombreCat { get; set; }

    public ICollection<Persona> Personas { get; set; }
}
