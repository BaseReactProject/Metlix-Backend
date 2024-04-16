using Application.Features.ContentDirectors.Commands.Create;
using Application.Features.ContentDirectors.Commands.Delete;
using Application.Features.ContentDirectors.Commands.Update;
using Application.Features.ContentDirectors.Queries.GetById;
using Application.Features.ContentDirectors.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentDirectorsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentDirectorCommand createContentDirectorCommand)
    {
        CreatedContentDirectorResponse response = await Mediator.Send(createContentDirectorCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentDirectorCommand updateContentDirectorCommand)
    {
        UpdatedContentDirectorResponse response = await Mediator.Send(updateContentDirectorCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedContentDirectorResponse response = await Mediator.Send(new DeleteContentDirectorCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdContentDirectorResponse response = await Mediator.Send(new GetByIdContentDirectorQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentDirectorQuery getListContentDirectorQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentDirectorListItemDto> response = await Mediator.Send(getListContentDirectorQuery);
        return Ok(response);
    }
}