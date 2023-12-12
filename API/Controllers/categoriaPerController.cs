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

public class CategoriaPerController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public CategoriaPerController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<CategoriaPerDto>>> Get()
    {
        var categoriaPer = await unitofwork.CategoriaPers.GetAllAsync();
        return mapper.Map<List<CategoriaPerDto>>(categoriaPer);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CategoriaPerDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.CategoriaPers.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<CategoriaPerDto>>(entidad.registros);
        return new Pager<CategoriaPerDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CategoriaPerDto>> Get(int id)
    {
        var CategoriaPer = await unitofwork.CategoriaPers.GetByIdAsync(id);
        if (CategoriaPer == null)
        {
            return NotFound();
        }
        return this.mapper.Map<CategoriaPerDto>(CategoriaPer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<CategoriaPer>> Post(CategoriaPerDto CategoriaPerDto)
    {
        var CategoriaPer = this.mapper.Map<CategoriaPer>(CategoriaPerDto);
        this.unitofwork.CategoriaPers.Add(CategoriaPer);
        await unitofwork.SaveAsync();
        if (CategoriaPer == null)
        {
            return BadRequest();
        }
        CategoriaPerDto.Id = CategoriaPer.Id;
        return CreatedAtAction(nameof(Post), new { id = CategoriaPerDto.Id }, CategoriaPerDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CategoriaPerDto>> Put(int id, [FromBody] CategoriaPerDto CategoriaPerDto)
    {
        if (CategoriaPerDto == null)
        {
            return NotFound();
        }
        var CategoriaPer = this.mapper.Map<CategoriaPer>(CategoriaPerDto);
        unitofwork.CategoriaPers.Update(CategoriaPer);
        await unitofwork.SaveAsync();
        return CategoriaPerDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var CategoriaPer = await unitofwork.CategoriaPers.GetByIdAsync(id);
        if (CategoriaPer == null)
        {
            return NotFound();
        }
        unitofwork.CategoriaPers.Remove(CategoriaPer);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
