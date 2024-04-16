using Core.Application.Responses;

namespace Application.Features.ContentScenarists.Commands.Delete;

public class DeletedContentScenaristResponse : IResponse
{
    public int Id { get; set; }
}