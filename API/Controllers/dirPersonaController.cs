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

public class DirPersonaController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public DirPersonaController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DirPersonaDto>>> Get()
    {
        var DirPersona = await unitofwork.DirPersonas.GetAllAsync();
        return mapper.Map<List<DirPersonaDto>>(DirPersona);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<DirPersonaDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.DirPersonas.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<DirPersonaDto>>(entidad.registros);
        return new Pager<DirPersonaDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DirPersonaDto>> Get(int id)
    {
        var DirPersona = await unitofwork.DirPersonas.GetByIdAsync(id);
        if (DirPersona == null)
        {
            return NotFound();
        }
        return this.mapper.Map<DirPersonaDto>(DirPersona);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<DirPersona>> Post(DirPersonaDto DirPersonaDto)
    {
        var DirPersona = this.mapper.Map<DirPersona>(DirPersonaDto);
        this.unitofwork.DirPersonas.Add(DirPersona);
        await unitofwork.SaveAsync();
        if (DirPersona == null)
        {
            return BadRequest();
        }
        DirPersonaDto.Id = DirPersona.Id;
        return CreatedAtAction(nameof(Post), new { id = DirPersonaDto.Id }, DirPersonaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DirPersonaDto>> Put(int id, [FromBody] DirPersonaDto DirPersonaDto)
    {
        if (DirPersonaDto == null)
        {
            return NotFound();
        }
        var DirPersona = this.mapper.Map<DirPersona>(DirPersonaDto);
        unitofwork.DirPersonas.Update(DirPersona);
        await unitofwork.SaveAsync();
        return DirPersonaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var DirPersona = await unitofwork.DirPersonas.GetByIdAsync(id);
        if (DirPersona == null)
        {
            return NotFound();
        }
        unitofwork.DirPersonas.Remove(DirPersona);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
