using Application.Features.AccountProfiles.Commands.Create;
using Application.Features.AccountProfiles.Commands.Delete;
using Application.Features.AccountProfiles.Commands.Update;
using Application.Features.AccountProfiles.Queries.GetById;
using Application.Features.AccountProfiles.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountProfilesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAccountProfileCommand createAccountProfileCommand)
    {
        CreatedAccountProfileResponse response = await Mediator.Send(createAccountProfileCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAccountProfileCommand updateAccountProfileCommand)
    {
        UpdatedAccountProfileResponse response = await Mediator.Send(updateAccountProfileCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedAccountProfileResponse response = await Mediator.Send(new DeleteAccountProfileCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdAccountProfileResponse response = await Mediator.Send(new GetByIdAccountProfileQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAccountProfileQuery getListAccountProfileQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAccountProfileListItemDto> response = await Mediator.Send(getListAccountProfileQuery);
        return Ok(response);
    }
}