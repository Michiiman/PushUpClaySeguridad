using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    public UnitOfWork(ApiContext context)
    {
        _context = context;
    }
    private readonly ApiContext _context;
    private RolRepository _Rol;
    private ProgramacionRepository _programaciones;
    private UsuarioRepository _usuarios;
    private PaisRepository _paises;
    private DepartamentoRepository _departamentos;
    private CiudadRepository _ciudads;
    private ContratoRepository _contratos;
    private EstadoRepository _estados;
    private PersonaRepository _personas;
    private DirPersonaRepository _dirPersonas;
    private TurnosRepository _turnos;
    private TipoDireccionRepository _tipoDirecciones;
    private ContactoPerRepository _contactoPers;
    private TipoPersonaRepository _tipoPersonas;
    private TipoContactoRepository _tipoContactos;
    private CategoriaPerRepository _categoriaPers;


    
    public IRol Roles
    {
        get
        {
            if (_Rol == null)
            {
                _Rol = new RolRepository(_context);
            }
            return _Rol;
        }
    }
    
    public IUsuario Usuarios
    {
        get
        {
            if (_usuarios == null)
            {
                _usuarios = new UsuarioRepository(_context);
            }
            return _usuarios;
        }
    }
    
    public IPais Paises
    {
        get
        {
            if (_paises == null)
            {
                _paises = new PaisRepository(_context);
            }
            return _paises;
        }
    }
    
    public IDepartamento Departamentos
    {
        get
        {
            if (_departamentos == null)
            {
                _departamentos = new DepartamentoRepository(_context);
            }
            return _departamentos;
        }
    }
    
    public ICiudad Ciudades
    {
        get
        {
            if (_ciudads == null)
            {
                _ciudads = new CiudadRepository(_context);
            }
            return _ciudads;
        }
    }
    
    public IContrato Contratos
    {
        get
        {
            if (_contratos == null)
            {
                _contratos = new ContratoRepository(_context);
            }
            return _contratos;
        }
    }
    public IEstado Estados
    {
        get
        {
            if (_estados == null)
            {
                _estados = new EstadoRepository(_context);
            }
            return _estados;
        }
    }
    
    public IPersona Personas
    {
        get
        {
            if (_personas == null)
            {
                _personas = new PersonaRepository(_context);
            }
            return _personas;
        }
    }
    public IDirPersona DirPersonas
    {
        get
        {
            if (_dirPersonas == null)
            {
                _dirPersonas = new DirPersonaRepository(_context);
            }
            return _dirPersonas;
        }
    }
    
    public IProgramacion Programaciones
    {
        get
        {
            if (_programaciones == null)
            {
                _programaciones = new ProgramacionRepository(_context);
            }
            return _programaciones;
        }
    }
    public ITurnos Turnos
    {
        get
        {
            if (_turnos == null)
            {
                _turnos = new TurnosRepository(_context);
            }
            return _turnos;
        }
    }
    
    public ITipoPersona TipoPersonas
    {
        get
        {
            if (_tipoPersonas == null)
            {
                _tipoPersonas = new TipoPersonaRepository(_context);
            }
            return _tipoPersonas;
        }
    }
    public IContactoPer ContactoPers
    {
        get
        {
            if (_contactoPers == null)
            {
                _contactoPers = new ContactoPerRepository(_context);
            }
            return _contactoPers;
        }
    }
    
    public ITipoContacto TipoContactos
    {
        get
        {
            if (_tipoContactos == null)
            {
                _tipoContactos = new TipoContactoRepository(_context);
            }
            return _tipoContactos;
        }
    }
    public ITipoDireccion TipoDirecciones
    {
        get
        {
            if (_tipoDirecciones == null)
            {
                _tipoDirecciones = new TipoDireccionRepository(_context);
            }
            return _tipoDirecciones;
        }
    }
    
    public ICategoriaPer CategoriaPers
    {
        get
        {
            if (_categoriaPers == null)
            {
                _categoriaPers = new CategoriaPerRepository(_context);
            }
            return _categoriaPers;
        }
    }
    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
