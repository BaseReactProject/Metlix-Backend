using Core.Application.Responses;

namespace Application.Features.ContentScenarists.Commands.Create;

public class CreatedContentScenaristResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}