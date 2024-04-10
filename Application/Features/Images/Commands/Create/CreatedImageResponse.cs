using Core.Application.Responses;

namespace Application.Features.Images.Commands.Create;

public class CreatedImageResponse : IResponse
{
    public CreatedImageResponse()
    {
        ImageUrl = string.Empty;
    }

    public CreatedImageResponse(int ýd, string ýmageUrl)
    {
        Id = ýd;
        ImageUrl = ýmageUrl;
    }

    public int Id { get; set; }
    public string ImageUrl { get; set; }
}