using Application.Features.Qualities.Commands.Create;
using Application.Features.Qualities.Commands.Delete;
using Application.Features.Qualities.Commands.Update;
using Application.Features.Qualities.Queries.GetById;
using Application.Features.Qualities.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QualitiesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateQualityCommand createQualityCommand)
    {
        CreatedQualityResponse response = await Mediator.Send(createQualityCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateQualityCommand updateQualityCommand)
    {
        UpdatedQualityResponse response = await Mediator.Send(updateQualityCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedQualityResponse response = await Mediator.Send(new DeleteQualityCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdQualityResponse response = await Mediator.Send(new GetByIdQualityQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListQualityQuery getListQualityQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListQualityListItemDto> response = await Mediator.Send(getListQualityQuery);
        return Ok(response);
    }
}