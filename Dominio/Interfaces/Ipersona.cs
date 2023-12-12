
using Dominio.Entities;
namespace Dominio.Interfaces;
public interface IPersona : IGenericRepo<Persona>
{
    Task<IEnumerable<Object>> AllEmpleados();
    Task<IEnumerable<Object>> EmpleadoVigilantes();
    Task<IEnumerable<Object>> EmpleadoVigilantesMasNumeros();
    Task<IEnumerable<Object>> ClientesBucaramanga();
    Task<IEnumerable<Object>> EmpleadosGironxPiedecuesta();
    Task<IEnumerable<Object>> Clientes5AñosAntiguedad();


}
