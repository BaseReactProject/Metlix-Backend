using Application.Features.Notices.Commands.Create;
using Application.Features.Notices.Commands.Delete;
using Application.Features.Notices.Commands.Update;
using Application.Features.Notices.Queries.GetById;
using Application.Features.Notices.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NoticesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateNoticeCommand createNoticeCommand)
    {
        CreatedNoticeResponse response = await Mediator.Send(createNoticeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateNoticeCommand updateNoticeCommand)
    {
        UpdatedNoticeResponse response = await Mediator.Send(updateNoticeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedNoticeResponse response = await Mediator.Send(new DeleteNoticeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdNoticeResponse response = await Mediator.Send(new GetByIdNoticeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListNoticeQuery getListNoticeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListNoticeListItemDto> response = await Mediator.Send(getListNoticeQuery);
        return Ok(response);
    }
}