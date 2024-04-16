using Application.Features.Trailers.Commands.Create;
using Application.Features.Trailers.Commands.Delete;
using Application.Features.Trailers.Commands.Update;
using Application.Features.Trailers.Queries.GetById;
using Application.Features.Trailers.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrailersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTrailerCommand createTrailerCommand)
    {
        CreatedTrailerResponse response = await Mediator.Send(createTrailerCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTrailerCommand updateTrailerCommand)
    {
        UpdatedTrailerResponse response = await Mediator.Send(updateTrailerCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedTrailerResponse response = await Mediator.Send(new DeleteTrailerCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdTrailerResponse response = await Mediator.Send(new GetByIdTrailerQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTrailerQuery getListTrailerQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTrailerListItemDto> response = await Mediator.Send(getListTrailerQuery);
        return Ok(response);
    }
}