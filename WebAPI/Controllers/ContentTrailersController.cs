using Application.Features.ContentTrailers.Commands.Create;
using Application.Features.ContentTrailers.Commands.Delete;
using Application.Features.ContentTrailers.Commands.Update;
using Application.Features.ContentTrailers.Queries.GetById;
using Application.Features.ContentTrailers.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentTrailersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentTrailerCommand createContentTrailerCommand)
    {
        CreatedContentTrailerResponse response = await Mediator.Send(createContentTrailerCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentTrailerCommand updateContentTrailerCommand)
    {
        UpdatedContentTrailerResponse response = await Mediator.Send(updateContentTrailerCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedContentTrailerResponse response = await Mediator.Send(new DeleteContentTrailerCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdContentTrailerResponse response = await Mediator.Send(new GetByIdContentTrailerQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentTrailerQuery getListContentTrailerQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentTrailerListItemDto> response = await Mediator.Send(getListContentTrailerQuery);
        return Ok(response);
    }
}