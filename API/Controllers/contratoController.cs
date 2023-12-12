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

public class ContratoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public ContratoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ContratoDto>>> Get()
    {
        var Contrato = await unitofwork.Contratos.GetAllAsync();
        return mapper.Map<List<ContratoDto>>(Contrato);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ContratoDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.Contratos.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<ContratoDto>>(entidad.registros);
        return new Pager<ContratoDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ContratoDto>> Get(int id)
    {
        var Contrato = await unitofwork.Contratos.GetByIdAsync(id);
        if (Contrato == null)
        {
            return NotFound();
        }
        return this.mapper.Map<ContratoDto>(Contrato);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Contrato>> Post(ContratoDto ContratoDto)
    {
        var Contrato = this.mapper.Map<Contrato>(ContratoDto);
        this.unitofwork.Contratos.Add(Contrato);
        await unitofwork.SaveAsync();
        if (Contrato == null)
        {
            return BadRequest();
        }
        ContratoDto.Id = Contrato.Id;
        return CreatedAtAction(nameof(Post), new { id = ContratoDto.Id }, ContratoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ContratoDto>> Put(int id, [FromBody] ContratoDto ContratoDto)
    {
        if (ContratoDto == null)
        {
            return NotFound();
        }
        var Contrato = this.mapper.Map<Contrato>(ContratoDto);
        unitofwork.Contratos.Update(Contrato);
        await unitofwork.SaveAsync();
        return ContratoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Contrato = await unitofwork.Contratos.GetByIdAsync(id);
        if (Contrato == null)
        {
            return NotFound();
        }
        unitofwork.Contratos.Remove(Contrato);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    //EndPoints
    [HttpGet("ContratosActivos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> ContratosActivos()
    {
        var contrato = await unitofwork.Contratos.ContratosActivos();
        return mapper.Map<List<Object>>(contrato);
    }
}
