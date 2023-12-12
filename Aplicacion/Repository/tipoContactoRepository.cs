
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class TipoContactoRepository  : GenericRepo<TipoContacto>, ITipoContacto
{
    protected readonly ApiContext _context;
    public TipoContactoRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<TipoContacto>> GetAllAsync()
    {
        return await _context.TipoContactos
            .ToListAsync();
    }
    public override async Task<TipoContacto> GetByIdAsync(int id)
    {
        return await _context.TipoContactos
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<TipoContacto> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.TipoContactos as IQueryable<TipoContacto>;
        if(string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Descripcion.ToLower().Contains(search));
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
