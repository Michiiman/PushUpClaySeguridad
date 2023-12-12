
using System.Collections;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
namespace Aplicacion.Repository;
public class PersonaRepository : GenericRepo<Persona>, IPersona
{
    protected readonly ApiContext _context;
    public PersonaRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _context.Personas
            .ToListAsync();
    }
    public override async Task<Persona> GetByIdAsync(int id)
    {
        return await _context.Personas
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Persona> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Personas as IQueryable<Persona>;
        if (string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }
        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }

    public async Task<IEnumerable<Object>> AllEmpleados()
    {
        var empleados = await (from p in _context.Personas
                            select new
                            {
                                Nombre = p.Nombre
                            }
        ).ToListAsync();

        return empleados;


    }
    public async Task<IEnumerable<Object>> EmpleadoVigilantes()
    {
        var empleados = await (from p in _context.Personas
                                join cat in _context.CategoriaPers on p.Id equals cat.Id
                                where cat.NombreCat == "Vigilante"
                            select new
                            {
                                Nombre = p.Nombre
                            }
        ).ToListAsync();

        return empleados;
    }
    public async Task<IEnumerable<Object>> EmpleadoVigilantesMasNumeros()
    {
        var empleados = await (from p in _context.Personas
                                join cat in _context.CategoriaPers on p.Id equals cat.Id
                                join contacto in _context.ContactoPers on p.Id equals contacto.IdPersonaFk 
                                where cat.NombreCat == "Vigilante"
                            select new
                            {
                                Nombre = p.Nombre,
                                Numeros= _context.ContactoPers
                                            .Where(c => c.IdPersonaFk == p.Id)
                                            .Select(c => c.Descripcion)
                                            .ToList()
                            }
        ).ToListAsync();

        return empleados;
    }
    public async Task<IEnumerable<Object>> ClientesBucaramanga()
    {
        var empleados = await (from p in _context.Personas
                                join ciudad in _context.Ciudades on p.Id equals ciudad.Id
                                where ciudad.NombreCiudad == "Bucaramanga"
                            select new
                            {
                                Nombre = p.Nombre,
                                ciudad=ciudad.NombreCiudad
                            }
        ).ToListAsync();

        return empleados;
    }
    public async Task<IEnumerable<Object>> EmpleadosGironxPiedecuesta()
    {
        var empleados = await (from p in _context.Personas
                                join ciudad in _context.Ciudades on p.Id equals ciudad.Id
                                where ciudad.NombreCiudad == "Giron" || ciudad.NombreCiudad == "Piedecuesta"
                            select new
                            {
                                Empleado = p.Nombre,
                                ciudad=ciudad.NombreCiudad
                            }
        ).ToListAsync();

        return empleados;
    }
    public async Task<IEnumerable<Object>> Clientes5AñosAntiguedad()
    {
        var empleados = await (from p in _context.Personas
                                where (DateTime.Now.Year - p.FechaRegistro.Year ) > 5
                            select new
                            {
                                Clientes = p.Nombre
                                
                            }
        ).ToListAsync();

        return empleados;
    }


}
