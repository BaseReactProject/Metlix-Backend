using Application.Features.Plans.Commands.Create;
using Application.Features.Plans.Commands.Delete;
using Application.Features.Plans.Commands.Update;
using Application.Features.Plans.Queries.GetById;
using Application.Features.Plans.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlansController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePlanCommand createPlanCommand)
    {
        CreatedPlanResponse response = await Mediator.Send(createPlanCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePlanCommand updatePlanCommand)
    {
        UpdatedPlanResponse response = await Mediator.Send(updatePlanCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedPlanResponse response = await Mediator.Send(new DeletePlanCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdPlanResponse response = await Mediator.Send(new GetByIdPlanQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPlanQuery getListPlanQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPlanListItemDto> response = await Mediator.Send(getListPlanQuery);
        return Ok(response);
    }
}