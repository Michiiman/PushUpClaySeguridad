
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
public class ContratoRepository : GenericRepo<Contrato>, IContrato
{
    protected readonly ApiContext _context;
    public ContratoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Contrato>> GetAllAsync()
    {
        return await _context.Contratos
            .ToListAsync();
    }
    public override async Task<Contrato> GetByIdAsync(int id)
    {
        return await _context.Contratos
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Contrato> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Contratos as IQueryable<Contrato>;
        if (string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Id.ToString().Contains(search));
        }
        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }

    //Endpoints

    public async Task<IEnumerable<Object>> ContratosActivos()
    {
        var contratos = await (from c in _context.Contratos
                            join estado in _context.Estados on c.IdEstadoFk equals estado.Id
                            where estado.Descripcion == "Activo"
                            select new
                            {
                                numeroContrato = c.Id,
                                Cliente = _context.Personas
                                            .Where(p => c.IdClienteFk == p.Id)
                                            .Select(p => p.Nombre)
                                            .ToList(),
                                Empleado = _context.Personas
                                            .Where(p => c.IdEmpleadoFk == p.Id)
                                            .Select(p=>p.Nombre)
                                            .ToList(),
                            }
        ).ToListAsync();

        return contratos;
    }

}
