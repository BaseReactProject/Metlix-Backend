using Application.Features.ContentActors.Commands.Create;
using Application.Features.ContentActors.Commands.Delete;
using Application.Features.ContentActors.Commands.Update;
using Application.Features.ContentActors.Queries.GetById;
using Application.Features.ContentActors.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentActorsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentActorCommand createContentActorCommand)
    {
        CreatedContentActorResponse response = await Mediator.Send(createContentActorCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentActorCommand updateContentActorCommand)
    {
        UpdatedContentActorResponse response = await Mediator.Send(updateContentActorCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedContentActorResponse response = await Mediator.Send(new DeleteContentActorCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdContentActorResponse response = await Mediator.Send(new GetByIdContentActorQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentActorQuery getListContentActorQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentActorListItemDto> response = await Mediator.Send(getListContentActorQuery);
        return Ok(response);
    }
}