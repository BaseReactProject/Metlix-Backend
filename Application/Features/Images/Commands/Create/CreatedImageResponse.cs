using Core.Application.Responses;

namespace Application.Features.Images.Commands.Create;

public class CreatedImageResponse : IResponse
{
    public CreatedImageResponse()
    {
        ImageUrl = string.Empty;
    }

    public CreatedImageResponse( string ýmageUrl)
    {
        ImageUrl = ýmageUrl;
    }

    public string ImageUrl { get; set; }
}