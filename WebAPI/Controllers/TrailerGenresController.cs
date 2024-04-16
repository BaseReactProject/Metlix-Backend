using Application.Features.TrailerGenres.Commands.Create;
using Application.Features.TrailerGenres.Commands.Delete;
using Application.Features.TrailerGenres.Commands.Update;
using Application.Features.TrailerGenres.Queries.GetById;
using Application.Features.TrailerGenres.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrailerGenresController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTrailerGenreCommand createTrailerGenreCommand)
    {
        CreatedTrailerGenreResponse response = await Mediator.Send(createTrailerGenreCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTrailerGenreCommand updateTrailerGenreCommand)
    {
        UpdatedTrailerGenreResponse response = await Mediator.Send(updateTrailerGenreCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedTrailerGenreResponse response = await Mediator.Send(new DeleteTrailerGenreCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdTrailerGenreResponse response = await Mediator.Send(new GetByIdTrailerGenreQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTrailerGenreQuery getListTrailerGenreQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTrailerGenreListItemDto> response = await Mediator.Send(getListTrailerGenreQuery);
        return Ok(response);
    }
}