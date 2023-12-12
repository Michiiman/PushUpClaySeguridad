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

public class PaisController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public PaisController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PaisDto>>> Get()
    {
        var Pais = await unitofwork.Paises.GetAllAsync();
        return mapper.Map<List<PaisDto>>(Pais);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PaisDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.Paises.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<PaisDto>>(entidad.registros);
        return new Pager<PaisDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PaisDto>> Get(int id)
    {
        var Pais = await unitofwork.Paises.GetByIdAsync(id);
        if (Pais == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PaisDto>(Pais);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pais>> Post(PaisDto PaisDto)
    {
        var Pais = this.mapper.Map<Pais>(PaisDto);
        this.unitofwork.Paises.Add(Pais);
        await unitofwork.SaveAsync();
        if (Pais == null)
        {
            return BadRequest();
        }
        PaisDto.Id = Pais.Id;
        return CreatedAtAction(nameof(Post), new { id = PaisDto.Id }, PaisDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PaisDto>> Put(int id, [FromBody] PaisDto PaisDto)
    {
        if (PaisDto == null)
        {
            return NotFound();
        }
        var Pais = this.mapper.Map<Pais>(PaisDto);
        unitofwork.Paises.Update(Pais);
        await unitofwork.SaveAsync();
        return PaisDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Pais = await unitofwork.Paises.GetByIdAsync(id);
        if (Pais == null)
        {
            return NotFound();
        }
        unitofwork.Paises.Remove(Pais);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
