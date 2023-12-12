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

public class TurnosController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public TurnosController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TurnosDto>>> Get()
    {
        var Turnos = await unitofwork.Turnos.GetAllAsync();
        return mapper.Map<List<TurnosDto>>(Turnos);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TurnosDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.Turnos.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<TurnosDto>>(entidad.registros);
        return new Pager<TurnosDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TurnosDto>> Get(int id)
    {
        var Turnos = await unitofwork.Turnos.GetByIdAsync(id);
        if (Turnos == null)
        {
            return NotFound();
        }
        return this.mapper.Map<TurnosDto>(Turnos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Turnos>> Post(TurnosDto TurnosDto)
    {
        var Turnos = this.mapper.Map<Turnos>(TurnosDto);
        this.unitofwork.Turnos.Add(Turnos);
        await unitofwork.SaveAsync();
        if (Turnos == null)
        {
            return BadRequest();
        }
        TurnosDto.Id = Turnos.Id;
        return CreatedAtAction(nameof(Post), new { id = TurnosDto.Id }, TurnosDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TurnosDto>> Put(int id, [FromBody] TurnosDto TurnosDto)
    {
        if (TurnosDto == null)
        {
            return NotFound();
        }
        var Turnos = this.mapper.Map<Turnos>(TurnosDto);
        unitofwork.Turnos.Update(Turnos);
        await unitofwork.SaveAsync();
        return TurnosDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Turnos = await unitofwork.Turnos.GetByIdAsync(id);
        if (Turnos == null)
        {
            return NotFound();
        }
        unitofwork.Turnos.Remove(Turnos);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
