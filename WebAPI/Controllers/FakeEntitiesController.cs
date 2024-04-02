using Application.Features.FakeEntities.Commands.Create;
using Application.Features.FakeEntities.Commands.Delete;
using Application.Features.FakeEntities.Commands.Update;
using Application.Features.FakeEntities.Queries.GetById;
using Application.Features.FakeEntities.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FakeEntitiesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFakeEntityCommand createFakeEntityCommand)
    {
        CreatedFakeEntityResponse response = await Mediator.Send(createFakeEntityCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFakeEntityCommand updateFakeEntityCommand)
    {
        UpdatedFakeEntityResponse response = await Mediator.Send(updateFakeEntityCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedFakeEntityResponse response = await Mediator.Send(new DeleteFakeEntityCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdFakeEntityResponse response = await Mediator.Send(new GetByIdFakeEntityQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFakeEntityQuery getListFakeEntityQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFakeEntityListItemDto> response = await Mediator.Send(getListFakeEntityQuery);
        return Ok(response);
    }
}