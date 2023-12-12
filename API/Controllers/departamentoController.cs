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

public class DepartamentoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public DepartamentoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Get()
    {
        var Departamento = await unitofwork.Departamentos.GetAllAsync();
        return mapper.Map<List<DepartamentoDto>>(Departamento);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<DepartamentoDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.Departamentos.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<DepartamentoDto>>(entidad.registros);
        return new Pager<DepartamentoDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DepartamentoDto>> Get(int id)
    {
        var Departamento = await unitofwork.Departamentos.GetByIdAsync(id);
        if (Departamento == null)
        {
            return NotFound();
        }
        return this.mapper.Map<DepartamentoDto>(Departamento);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Departamento>> Post(DepartamentoDto DepartamentoDto)
    {
        var Departamento = this.mapper.Map<Departamento>(DepartamentoDto);
        this.unitofwork.Departamentos.Add(Departamento);
        await unitofwork.SaveAsync();
        if (Departamento == null)
        {
            return BadRequest();
        }
        DepartamentoDto.Id = Departamento.Id;
        return CreatedAtAction(nameof(Post), new { id = DepartamentoDto.Id }, DepartamentoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DepartamentoDto>> Put(int id, [FromBody] DepartamentoDto DepartamentoDto)
    {
        if (DepartamentoDto == null)
        {
            return NotFound();
        }
        var Departamento = this.mapper.Map<Departamento>(DepartamentoDto);
        unitofwork.Departamentos.Update(Departamento);
        await unitofwork.SaveAsync();
        return DepartamentoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Departamento = await unitofwork.Departamentos.GetByIdAsync(id);
        if (Departamento == null)
        {
            return NotFound();
        }
        unitofwork.Departamentos.Remove(Departamento);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
