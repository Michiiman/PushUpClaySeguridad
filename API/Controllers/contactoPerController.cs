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

public class ContactoPerController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public ContactoPerController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ContactoPerDto>>> Get()
    {
        var ContactoPer = await unitofwork.ContactoPers.GetAllAsync();
        return mapper.Map<List<ContactoPerDto>>(ContactoPer);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ContactoPerDto>>> GetPagination([FromQuery] Params EntidadParams)
    {
        var entidad = await unitofwork.ContactoPers.GetAllAsync(EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
        var listEntidad = mapper.Map<List<ContactoPerDto>>(entidad.registros);
        return new Pager<ContactoPerDto>(listEntidad, entidad.totalRegistros, EntidadParams.PageIndex, EntidadParams.PageSize, EntidadParams.Search);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ContactoPerDto>> Get(int id)
    {
        var ContactoPer = await unitofwork.ContactoPers.GetByIdAsync(id);
        if (ContactoPer == null)
        {
            return NotFound();
        }
        return this.mapper.Map<ContactoPerDto>(ContactoPer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ContactoPer>> Post(ContactoPerDto ContactoPerDto)
    {
        var ContactoPer = this.mapper.Map<ContactoPer>(ContactoPerDto);
        this.unitofwork.ContactoPers.Add(ContactoPer);
        await unitofwork.SaveAsync();
        if (ContactoPer == null)
        {
            return BadRequest();
        }
        ContactoPerDto.Id = ContactoPer.Id;
        return CreatedAtAction(nameof(Post), new { id = ContactoPerDto.Id }, ContactoPerDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ContactoPerDto>> Put(int id, [FromBody] ContactoPerDto ContactoPerDto)
    {
        if (ContactoPerDto == null)
        {
            return NotFound();
        }
        var ContactoPer = this.mapper.Map<ContactoPer>(ContactoPerDto);
        unitofwork.ContactoPers.Update(ContactoPer);
        await unitofwork.SaveAsync();
        return ContactoPerDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var ContactoPer = await unitofwork.ContactoPers.GetByIdAsync(id);
        if (ContactoPer == null)
        {
            return NotFound();
        }
        unitofwork.ContactoPers.Remove(ContactoPer);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
