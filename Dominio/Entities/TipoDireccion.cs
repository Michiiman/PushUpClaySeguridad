namespace Dominio.Entities;
public class TipoDireccion : BaseEntity
{
    public string Descripcion { get; set; }
    public ICollection<DirPersona> DirPersonas { get; set; }

}
