
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class TurnosRepository  : GenericRepo<Turnos>, ITurnos
{
    protected readonly ApiContext _context;
    public TurnosRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Turnos>> GetAllAsync()
    {
        return await _context.Turnos
            .ToListAsync();
    }
    public override async Task<Turnos> GetByIdAsync(int id)
    {
        return await _context.Turnos
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Turnos> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Turnos as IQueryable<Turnos>;
        if(string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreTurno.Contains(search));
        }
        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
