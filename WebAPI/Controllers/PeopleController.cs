using Application.Features.People.Commands.Create;
using Application.Features.People.Commands.Delete;
using Application.Features.People.Commands.Update;
using Application.Features.People.Queries.GetById;
using Application.Features.People.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePersonCommand createPersonCommand)
    {
        CreatedPersonResponse response = await Mediator.Send(createPersonCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePersonCommand updatePersonCommand)
    {
        UpdatedPersonResponse response = await Mediator.Send(updatePersonCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedPersonResponse response = await Mediator.Send(new DeletePersonCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdPersonResponse response = await Mediator.Send(new GetByIdPersonQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPersonQuery getListPersonQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPersonListItemDto> response = await Mediator.Send(getListPersonQuery);
        return Ok(response);
    }
}