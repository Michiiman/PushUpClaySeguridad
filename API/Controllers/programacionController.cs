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

public class ProgramacionController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public ProgramacionController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ProgramacionDto>>> Get()
    {
        var Programacion = await unitofwork.Programaciones.GetAllAsync();
        return mapper.Map<List<ProgramacionDto>>(Programacion);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProgramacionDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.Programaciones.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<ProgramacionDto>>(entidad.registros);
        return new Pager<ProgramacionDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProgramacionDto>> Get(int id)
    {
        var Programacion = await unitofwork.Programaciones.GetByIdAsync(id);
        if (Programacion == null)
        {
            return NotFound();
        }
        return this.mapper.Map<ProgramacionDto>(Programacion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Programacion>> Post(ProgramacionDto ProgramacionDto)
    {
        var Programacion = this.mapper.Map<Programacion>(ProgramacionDto);
        this.unitofwork.Programaciones.Add(Programacion);
        await unitofwork.SaveAsync();
        if (Programacion == null)
        {
            return BadRequest();
        }
        ProgramacionDto.Id = Programacion.Id;
        return CreatedAtAction(nameof(Post), new { id = ProgramacionDto.Id }, ProgramacionDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProgramacionDto>> Put(int id, [FromBody] ProgramacionDto ProgramacionDto)
    {
        if (ProgramacionDto == null)
        {
            return NotFound();
        }
        var Programacion = this.mapper.Map<Programacion>(ProgramacionDto);
        unitofwork.Programaciones.Update(Programacion);
        await unitofwork.SaveAsync();
        return ProgramacionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Programacion = await unitofwork.Programaciones.GetByIdAsync(id);
        if (Programacion == null)
        {
            return NotFound();
        }
        unitofwork.Programaciones.Remove(Programacion);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
