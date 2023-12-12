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

public class CiudadController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public CiudadController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<CiudadDto>>> Get()
    {
        var Ciudad = await unitofwork.Ciudades.GetAllAsync();
        return mapper.Map<List<CiudadDto>>(Ciudad);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CiudadDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.Ciudades.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<CiudadDto>>(entidad.registros);
        return new Pager<CiudadDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CiudadDto>> Get(int id)
    {
        var Ciudad = await unitofwork.Ciudades.GetByIdAsync(id);
        if (Ciudad == null)
        {
            return NotFound();
        }
        return this.mapper.Map<CiudadDto>(Ciudad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Ciudad>> Post(CiudadDto CiudadDto)
    {
        var Ciudad = this.mapper.Map<Ciudad>(CiudadDto);
        this.unitofwork.Ciudades.Add(Ciudad);
        await unitofwork.SaveAsync();
        if (Ciudad == null)
        {
            return BadRequest();
        }
        CiudadDto.Id = Ciudad.Id;
        return CreatedAtAction(nameof(Post), new { id = CiudadDto.Id }, CiudadDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CiudadDto>> Put(int id, [FromBody] CiudadDto CiudadDto)
    {
        if (CiudadDto == null)
        {
            return NotFound();
        }
        var Ciudad = this.mapper.Map<Ciudad>(CiudadDto);
        unitofwork.Ciudades.Update(Ciudad);
        await unitofwork.SaveAsync();
        return CiudadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Ciudad = await unitofwork.Ciudades.GetByIdAsync(id);
        if (Ciudad == null)
        {
            return NotFound();
        }
        unitofwork.Ciudades.Remove(Ciudad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
