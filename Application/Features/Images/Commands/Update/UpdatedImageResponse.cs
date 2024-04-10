using Core.Application.Responses;

namespace Application.Features.Images.Commands.Update;

public class UpdatedImageResponse : IResponse
{
    public UpdatedImageResponse()
    {
        ImageUrl = string.Empty;
    }

    public UpdatedImageResponse(int �d, string �mageUrl)
    {
        Id = �d;
        ImageUrl = �mageUrl;
    }

    public int Id { get; set; }
    public string ImageUrl { get; set; }
}