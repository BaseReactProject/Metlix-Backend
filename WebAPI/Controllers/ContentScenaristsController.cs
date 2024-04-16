using Application.Features.ContentScenarists.Commands.Create;
using Application.Features.ContentScenarists.Commands.Delete;
using Application.Features.ContentScenarists.Commands.Update;
using Application.Features.ContentScenarists.Queries.GetById;
using Application.Features.ContentScenarists.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentScenaristsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentScenaristCommand createContentScenaristCommand)
    {
        CreatedContentScenaristResponse response = await Mediator.Send(createContentScenaristCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentScenaristCommand updateContentScenaristCommand)
    {
        UpdatedContentScenaristResponse response = await Mediator.Send(updateContentScenaristCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedContentScenaristResponse response = await Mediator.Send(new DeleteContentScenaristCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdContentScenaristResponse response = await Mediator.Send(new GetByIdContentScenaristQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentScenaristQuery getListContentScenaristQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentScenaristListItemDto> response = await Mediator.Send(getListContentScenaristQuery);
        return Ok(response);
    }
}