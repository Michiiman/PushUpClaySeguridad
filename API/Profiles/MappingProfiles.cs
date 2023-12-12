using API.Dtos;
using AutoMapper;
using Dominio.Entities;
namespace API.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Rol, RolDto>().ReverseMap();
        CreateMap<Usuario, UsuarioDto>().ReverseMap();
        CreateMap<Pais, PaisDto>().ReverseMap();
        CreateMap<Departamento, DepartamentoDto>().ReverseMap();
        CreateMap<Ciudad, CiudadDto>().ReverseMap();
        CreateMap<Contrato, ContratoDto>().ReverseMap();
        CreateMap<Estado, EstadoDto>().ReverseMap();
        CreateMap<Persona, PersonaDto>().ReverseMap();
        CreateMap<DirPersona, DirPersonaDto>().ReverseMap();
        CreateMap<Programacion, ProgramacionDto>().ReverseMap();
        CreateMap<Turnos, TurnosDto>().ReverseMap();
        CreateMap<TipoPersona, TipoPersonaDto>().ReverseMap();
        CreateMap<ContactoPer, ContactoPerDto>().ReverseMap();
        CreateMap<TipoContacto, TipoContactoDto>().ReverseMap();
        CreateMap<TipoDireccion, TipoDireccionDto>().ReverseMap();
        CreateMap<CategoriaPer, CategoriaPerDto>().ReverseMap();
    }
}
