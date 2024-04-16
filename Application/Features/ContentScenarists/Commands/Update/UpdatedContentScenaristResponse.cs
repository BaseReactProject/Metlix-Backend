using Core.Application.Responses;

namespace Application.Features.ContentScenarists.Commands.Update;

public class UpdatedContentScenaristResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}