
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class CategoriaPerRepository  : GenericRepo<CategoriaPer>, ICategoriaPer
{
    protected readonly ApiContext _context;
    public CategoriaPerRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<CategoriaPer>> GetAllAsync()
    {
        return await _context.CategoriaPers
            .ToListAsync();
    }
    public override async Task<CategoriaPer> GetByIdAsync(int id)
    {
        return await _context.CategoriaPers
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<CategoriaPer> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.CategoriaPers as IQueryable<CategoriaPer>;
        if(string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreCat.Contains(search));
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
