using Core.Application.Responses;

namespace Application.Features.Images.Commands.Create;

public class CreatedImageResponse : IResponse
{
    public CreatedImageResponse()
    {
        ImageUrl = string.Empty;
    }

    public CreatedImageResponse(int �d, string �mageUrl)
    {
        Id = �d;
        ImageUrl = �mageUrl;
    }

    public int Id { get; set; }
    public string ImageUrl { get; set; }
}