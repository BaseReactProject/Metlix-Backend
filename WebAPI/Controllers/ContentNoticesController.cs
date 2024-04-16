using Application.Features.ContentNotices.Commands.Create;
using Application.Features.ContentNotices.Commands.Delete;
using Application.Features.ContentNotices.Commands.Update;
using Application.Features.ContentNotices.Queries.GetById;
using Application.Features.ContentNotices.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentNoticesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentNoticeCommand createContentNoticeCommand)
    {
        CreatedContentNoticeResponse response = await Mediator.Send(createContentNoticeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentNoticeCommand updateContentNoticeCommand)
    {
        UpdatedContentNoticeResponse response = await Mediator.Send(updateContentNoticeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedContentNoticeResponse response = await Mediator.Send(new DeleteContentNoticeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdContentNoticeResponse response = await Mediator.Send(new GetByIdContentNoticeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentNoticeQuery getListContentNoticeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentNoticeListItemDto> response = await Mediator.Send(getListContentNoticeQuery);
        return Ok(response);
    }
}