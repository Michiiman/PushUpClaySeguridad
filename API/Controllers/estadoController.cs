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

public class EstadoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public EstadoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<EstadoDto>>> Get()
    {
        var Estado = await unitofwork.Estados.GetAllAsync();
        return mapper.Map<List<EstadoDto>>(Estado);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<EstadoDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.Estados.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<EstadoDto>>(entidad.registros);
        return new Pager<EstadoDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EstadoDto>> Get(int id)
    {
        var Estado = await unitofwork.Estados.GetByIdAsync(id);
        if (Estado == null)
        {
            return NotFound();
        }
        return this.mapper.Map<EstadoDto>(Estado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Estado>> Post(EstadoDto EstadoDto)
    {
        var Estado = this.mapper.Map<Estado>(EstadoDto);
        this.unitofwork.Estados.Add(Estado);
        await unitofwork.SaveAsync();
        if (Estado == null)
        {
            return BadRequest();
        }
        EstadoDto.Id = Estado.Id;
        return CreatedAtAction(nameof(Post), new { id = EstadoDto.Id }, EstadoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EstadoDto>> Put(int id, [FromBody] EstadoDto EstadoDto)
    {
        if (EstadoDto == null)
        {
            return NotFound();
        }
        var Estado = this.mapper.Map<Estado>(EstadoDto);
        unitofwork.Estados.Update(Estado);
        await unitofwork.SaveAsync();
        return EstadoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Estado = await unitofwork.Estados.GetByIdAsync(id);
        if (Estado == null)
        {
            return NotFound();
        }
        unitofwork.Estados.Remove(Estado);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
