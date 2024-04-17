using Application.Features.ContentOutroes.Commands.Create;
using Application.Features.ContentOutroes.Commands.Delete;
using Application.Features.ContentOutroes.Commands.Update;
using Application.Features.ContentOutroes.Queries.GetById;
using Application.Features.ContentOutroes.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentOutroesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentOutroCommand createContentOutroCommand)
    {
        CreatedContentOutroResponse response = await Mediator.Send(createContentOutroCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentOutroCommand updateContentOutroCommand)
    {
        UpdatedContentOutroResponse response = await Mediator.Send(updateContentOutroCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedContentOutroResponse response = await Mediator.Send(new DeleteContentOutroCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdContentOutroResponse response = await Mediator.Send(new GetByIdContentOutroQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentOutroQuery getListContentOutroQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentOutroListItemDto> response = await Mediator.Send(getListContentOutroQuery);
        return Ok(response);
    }
}