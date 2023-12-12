namespace Dominio.Entities;
public class TipoContacto : BaseEntity
{
    public string Descripcion { get; set; }
    public ICollection<ContactoPer> ContactoPersonas { get; set; }
    
}

