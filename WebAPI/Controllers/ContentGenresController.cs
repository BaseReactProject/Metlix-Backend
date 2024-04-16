using Application.Features.ContentGenres.Commands.Create;
using Application.Features.ContentGenres.Commands.Delete;
using Application.Features.ContentGenres.Commands.Update;
using Application.Features.ContentGenres.Queries.GetById;
using Application.Features.ContentGenres.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentGenresController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentGenreCommand createContentGenreCommand)
    {
        CreatedContentGenreResponse response = await Mediator.Send(createContentGenreCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentGenreCommand updateContentGenreCommand)
    {
        UpdatedContentGenreResponse response = await Mediator.Send(updateContentGenreCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedContentGenreResponse response = await Mediator.Send(new DeleteContentGenreCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdContentGenreResponse response = await Mediator.Send(new GetByIdContentGenreQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentGenreQuery getListContentGenreQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentGenreListItemDto> response = await Mediator.Send(getListContentGenreQuery);
        return Ok(response);
    }
}