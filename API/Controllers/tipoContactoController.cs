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

public class TipoContactoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public TipoContactoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoContactoDto>>> Get()
    {
        var TipoContacto = await unitofwork.TipoContactos.GetAllAsync();
        return mapper.Map<List<TipoContactoDto>>(TipoContacto);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoContactoDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.TipoContactos.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<TipoContactoDto>>(entidad.registros);
        return new Pager<TipoContactoDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoContactoDto>> Get(int id)
    {
        var TipoContacto = await unitofwork.TipoContactos.GetByIdAsync(id);
        if (TipoContacto == null)
        {
            return NotFound();
        }
        return this.mapper.Map<TipoContactoDto>(TipoContacto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoContacto>> Post(TipoContactoDto TipoContactoDto)
    {
        var TipoContacto = this.mapper.Map<TipoContacto>(TipoContactoDto);
        this.unitofwork.TipoContactos.Add(TipoContacto);
        await unitofwork.SaveAsync();
        if (TipoContacto == null)
        {
            return BadRequest();
        }
        TipoContactoDto.Id = TipoContacto.Id;
        return CreatedAtAction(nameof(Post), new { id = TipoContactoDto.Id }, TipoContactoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoContactoDto>> Put(int id, [FromBody] TipoContactoDto TipoContactoDto)
    {
        if (TipoContactoDto == null)
        {
            return NotFound();
        }
        var TipoContacto = this.mapper.Map<TipoContacto>(TipoContactoDto);
        unitofwork.TipoContactos.Update(TipoContacto);
        await unitofwork.SaveAsync();
        return TipoContactoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var TipoContacto = await unitofwork.TipoContactos.GetByIdAsync(id);
        if (TipoContacto == null)
        {
            return NotFound();
        }
        unitofwork.TipoContactos.Remove(TipoContacto);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
