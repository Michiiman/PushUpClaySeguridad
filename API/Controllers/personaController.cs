using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
//[Authorize]

public class PersonaController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public PersonaController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get()
    {
        var Persona = await unitofwork.Personas.GetAllAsync();
        return mapper.Map<List<PersonaDto>>(Persona);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PersonaDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.Personas.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<PersonaDto>>(entidad.registros);
        return new Pager<PersonaDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PersonaDto>> Get(int id)
    {
        var Persona = await unitofwork.Personas.GetByIdAsync(id);
        if (Persona == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PersonaDto>(Persona);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Persona>> Post(PersonaDto PersonaDto)
    {
        var Persona = this.mapper.Map<Persona>(PersonaDto);
        this.unitofwork.Personas.Add(Persona);
        await unitofwork.SaveAsync();
        if (Persona == null)
        {
            return BadRequest();
        }
        PersonaDto.Id = Persona.Id;
        return CreatedAtAction(nameof(Post), new { id = PersonaDto.Id }, PersonaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PersonaDto>> Put(int id, [FromBody] PersonaDto PersonaDto)
    {
        if (PersonaDto == null)
        {
            return NotFound();
        }
        var Persona = this.mapper.Map<Persona>(PersonaDto);
        unitofwork.Personas.Update(Persona);
        await unitofwork.SaveAsync();
        return PersonaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Persona = await unitofwork.Personas.GetByIdAsync(id);
        if (Persona == null)
        {
            return NotFound();
        }
        unitofwork.Personas.Remove(Persona);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    //EndPoints
    //1.Listar todos los empleados de la empresa de seguridad
    [HttpGet("AllEmpleados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> AllEmpleados()
    {
        var Persona = await unitofwork.Personas.AllEmpleados();
        return mapper.Map<List<Object>>(Persona);
    }

    //2.Listar todos los empleados que son vigilantes
    [HttpGet("EmpleadoVigilantes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> EmpleadoVigilantes()
    {
        var Persona = await unitofwork.Personas.EmpleadoVigilantes();
        return mapper.Map<List<Object>>(Persona);
    }

    //3.Listar los numeros de contacto de un empleado que sea vigilante
    [HttpGet("EmpleadoVigilantesMasNumeros")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> EmpleadoVigilantesMasNumeros()
    {
        var Persona = await unitofwork.Personas.EmpleadoVigilantesMasNumeros();
        return mapper.Map<List<Object>>(Persona);
    }

    //4.Listar todos los cleientes que vivan en la ciudad de bucaramanga
    [HttpGet("ClientesBucaramanga")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> ClientesBucaramanga()
    {
        var Persona = await unitofwork.Personas.ClientesBucaramanga();
        return mapper.Map<List<Object>>(Persona);
    }


    //5.Listar todos los empleados que vivan en la ciudad de giron y piedecuesta
    [HttpGet("EmpleadosGironxPiedecuesta")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> EmpleadosGironxPiedecuesta()
    {
        var Persona = await unitofwork.Personas.EmpleadosGironxPiedecuesta();
        return mapper.Map<List<Object>>(Persona);
    }

    //6.Listar todos los clientes que tengas mas de  5 años de antiguedad
    [HttpGet("Clientes5AñosAntiguedad")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> Clientes5AñosAntiguedad()
    {
        var Persona = await unitofwork.Personas.Clientes5AñosAntiguedad();
        return mapper.Map<List<Object>>(Persona);
    }
    


}

