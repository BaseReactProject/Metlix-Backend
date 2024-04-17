using Application.Features.Movies.Commands.Create;
using Application.Features.Movies.Commands.Delete;
using Application.Features.Movies.Commands.Update;
using Application.Features.Movies.Queries.GetById;
using Application.Features.Movies.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] CreateMovieCommand createMovieCommand)
    {
        CreatedMovieResponse response = await Mediator.Send(createMovieCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMovieCommand updateMovieCommand)
    {
        UpdatedMovieResponse response = await Mediator.Send(updateMovieCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedMovieResponse response = await Mediator.Send(new DeleteMovieCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdMovieResponse response = await Mediator.Send(new GetByIdMovieQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMovieQuery getListMovieQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListMovieListItemDto> response = await Mediator.Send(getListMovieQuery);
        return Ok(response);
    }
}