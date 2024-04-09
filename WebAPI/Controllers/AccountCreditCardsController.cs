using Application.Features.AccountCreditCards.Commands.Create;
using Application.Features.AccountCreditCards.Commands.Delete;
using Application.Features.AccountCreditCards.Commands.Update;
using Application.Features.AccountCreditCards.Queries.GetById;
using Application.Features.AccountCreditCards.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountCreditCardsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAccountCreditCardCommand createAccountCreditCardCommand)
    {
        CreatedAccountCreditCardResponse response = await Mediator.Send(createAccountCreditCardCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAccountCreditCardCommand updateAccountCreditCardCommand)
    {
        UpdatedAccountCreditCardResponse response = await Mediator.Send(updateAccountCreditCardCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedAccountCreditCardResponse response = await Mediator.Send(new DeleteAccountCreditCardCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdAccountCreditCardResponse response = await Mediator.Send(new GetByIdAccountCreditCardQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAccountCreditCardQuery getListAccountCreditCardQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAccountCreditCardListItemDto> response = await Mediator.Send(getListAccountCreditCardQuery);
        return Ok(response);
    }
}