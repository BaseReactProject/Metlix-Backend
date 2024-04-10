using Core.Application.Responses;

namespace Application.Features.Images.Commands.Create;

public class CreatedImageResponse : IResponse
{
    public CreatedImageResponse()
    {
        ImageUrl = string.Empty;
    }

    public CreatedImageResponse( string �mageUrl)
    {
        ImageUrl = �mageUrl;
    }

    public string ImageUrl { get; set; }
}