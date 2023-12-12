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
[Authorize]

public class TipoPersonaController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public TipoPersonaController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoPersonaDto>>> Get()
    {
        var TipoPersona = await unitofwork.TipoPersonas.GetAllAsync();
        return mapper.Map<List<TipoPersonaDto>>(TipoPersona);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoPersonaDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.TipoPersonas.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<TipoPersonaDto>>(entidad.registros);
        return new Pager<TipoPersonaDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoPersonaDto>> Get(int id)
    {
        var TipoPersona = await unitofwork.TipoPersonas.GetByIdAsync(id);
        if (TipoPersona == null)
        {
            return NotFound();
        }
        return this.mapper.Map<TipoPersonaDto>(TipoPersona);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoPersona>> Post(TipoPersonaDto TipoPersonaDto)
    {
        var TipoPersona = this.mapper.Map<TipoPersona>(TipoPersonaDto);
        this.unitofwork.TipoPersonas.Add(TipoPersona);
        await unitofwork.SaveAsync();
        if (TipoPersona == null)
        {
            return BadRequest();
        }
        TipoPersonaDto.Id = TipoPersona.Id;
        return CreatedAtAction(nameof(Post), new { id = TipoPersonaDto.Id }, TipoPersonaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoPersonaDto>> Put(int id, [FromBody] TipoPersonaDto TipoPersonaDto)
    {
        if (TipoPersonaDto == null)
        {
            return NotFound();
        }
        var TipoPersona = this.mapper.Map<TipoPersona>(TipoPersonaDto);
        unitofwork.TipoPersonas.Update(TipoPersona);
        await unitofwork.SaveAsync();
        return TipoPersonaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var TipoPersona = await unitofwork.TipoPersonas.GetByIdAsync(id);
        if (TipoPersona == null)
        {
            return NotFound();
        }
        unitofwork.TipoPersonas.Remove(TipoPersona);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
