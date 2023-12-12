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

public class TipoDireccionController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public TipoDireccionController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoDireccionDto>>> Get()
    {
        var TipoDireccion = await unitofwork.TipoDirecciones.GetAllAsync();
        return mapper.Map<List<TipoDireccionDto>>(TipoDireccion);
    }
   [HttpGet]
   [MapToApiVersion("1.1")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<Pager<TipoDireccionDto>>> GetPagination([FromQuery] Params EntidadParams)
   {
       var entidad = await unitofwork.TipoDirecciones.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
       var listEntidad = mapper.Map<List<TipoDireccionDto>>(entidad.registros);
       return new Pager<TipoDireccionDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
   }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoDireccionDto>> Get(int id)
    {
        var TipoDireccion = await unitofwork.TipoDirecciones.GetByIdAsync(id);
        if (TipoDireccion == null){
            return NotFound();
        }
        return this.mapper.Map<TipoDireccionDto>(TipoDireccion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoDireccion>> Post(TipoDireccionDto TipoDireccionDto)
    {
        var TipoDireccion = this.mapper.Map<TipoDireccion>(TipoDireccionDto);
        this.unitofwork.TipoDirecciones.Add(TipoDireccion);
        await unitofwork.SaveAsync();
        if(TipoDireccion == null)
        {
            return BadRequest();
        }
        TipoDireccionDto.Id = TipoDireccion.Id;
        return CreatedAtAction(nameof(Post), new {id = TipoDireccionDto.Id}, TipoDireccionDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoDireccionDto>> Put(int id, [FromBody]TipoDireccionDto TipoDireccionDto){
        if(TipoDireccionDto == null)
        {
            return NotFound();
        }
        var TipoDireccion = this.mapper.Map<TipoDireccion>(TipoDireccionDto);
        unitofwork.TipoDirecciones.Update(TipoDireccion);
        await unitofwork.SaveAsync();
        return TipoDireccionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var TipoDireccion = await unitofwork.TipoDirecciones.GetByIdAsync(id);
        if(TipoDireccion == null)
        {
            return NotFound();
        }
        unitofwork.TipoDirecciones.Remove(TipoDireccion);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
