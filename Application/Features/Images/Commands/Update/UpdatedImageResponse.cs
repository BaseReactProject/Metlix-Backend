using Core.Application.Responses;

namespace Application.Features.Images.Commands.Update;

public class UpdatedImageResponse : IResponse
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
}