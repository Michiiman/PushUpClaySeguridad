namespace Dominio.Interfaces;
public interface IUnitOfWork
{
   IRol Roles { get; }
   IUsuario Usuarios { get; }
   IPais Paises { get; }
   IDepartamento Departamentos { get; }
   ICiudad Ciudades { get; }
   IContrato Contratos { get; }
   IEstado Estados { get; }
   IPersona Personas { get; }
   IDirPersona DirPersonas { get; }
   IProgramacion Programaciones { get; }
   ITurnos Turnos { get; }
   ITipoPersona TipoPersonas { get; }
   IContactoPer ContactoPers { get; }
   ITipoContacto TipoContactos { get; }
   ITipoDireccion TipoDirecciones { get; }
   ICategoriaPer CategoriaPers { get; }
Task<int> SaveAsync();
}
