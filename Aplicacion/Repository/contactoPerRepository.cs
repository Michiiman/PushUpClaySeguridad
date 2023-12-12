
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
    public class ContactoPerRepository  : GenericRepo<ContactoPer>, IContactoPer
{
    protected readonly ApiContext _context;
    public ContactoPerRepository(ApiContext context) : base (context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<ContactoPer>> GetAllAsync()
    {
        return await _context.ContactoPers
            .ToListAsync();
    }
    public override async Task<ContactoPer> GetByIdAsync(int id)
    {
        return await _context.ContactoPers
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<ContactoPer> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.ContactoPers as IQueryable<ContactoPer>;
        if(string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Descripcion.Contains(search));
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
