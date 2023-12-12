
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class DirPersonaRepository  : GenericRepo<DirPersona>, IDirPersona
{
    protected readonly ApiContext _context;
    public DirPersonaRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<DirPersona>> GetAllAsync()
    {
        return await _context.DirPersonas
            .ToListAsync();
    }
    public override async Task<DirPersona> GetByIdAsync(int id)
    {
        return await _context.DirPersonas
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<DirPersona> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.DirPersonas as IQueryable<DirPersona>;
        if(string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Direccion.Contains(search));
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
