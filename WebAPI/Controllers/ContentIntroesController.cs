using Application.Features.ContentIntroes.Commands.Create;
using Application.Features.ContentIntroes.Commands.Delete;
using Application.Features.ContentIntroes.Commands.Update;
using Application.Features.ContentIntroes.Queries.GetById;
using Application.Features.ContentIntroes.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentIntroesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentIntroCommand createContentIntroCommand)
    {
        CreatedContentIntroResponse response = await Mediator.Send(createContentIntroCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentIntroCommand updateContentIntroCommand)
    {
        UpdatedContentIntroResponse response = await Mediator.Send(updateContentIntroCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedContentIntroResponse response = await Mediator.Send(new DeleteContentIntroCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdContentIntroResponse response = await Mediator.Send(new GetByIdContentIntroQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentIntroQuery getListContentIntroQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentIntroListItemDto> response = await Mediator.Send(getListContentIntroQuery);
        return Ok(response);
    }
}