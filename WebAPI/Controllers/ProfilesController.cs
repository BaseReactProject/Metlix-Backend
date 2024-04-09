using Application.Features.Profiles.Commands.Create;
using Application.Features.Profiles.Commands.Delete;
using Application.Features.Profiles.Commands.Update;
using Application.Features.Profiles.Queries.GetById;
using Application.Features.Profiles.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProfileCommand createProfileCommand)
    {
        CreatedProfileResponse response = await Mediator.Send(createProfileCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProfileCommand updateProfileCommand)
    {
        UpdatedProfileResponse response = await Mediator.Send(updateProfileCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedProfileResponse response = await Mediator.Send(new DeleteProfileCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdProfileResponse response = await Mediator.Send(new GetByIdProfileQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProfileQuery getListProfileQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListProfileListItemDto> response = await Mediator.Send(getListProfileQuery);
        return Ok(response);
    }
}