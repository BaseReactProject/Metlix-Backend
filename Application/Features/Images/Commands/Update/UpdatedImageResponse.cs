using Core.Application.Responses;

namespace Application.Features.Images.Commands.Update;

public class UpdatedImageResponse : IResponse
{
    public UpdatedImageResponse()
    {
        ImageUrl = string.Empty;
    }

    public UpdatedImageResponse(int ýd, string ýmageUrl)
    {
        Id = ýd;
        ImageUrl = ýmageUrl;
    }

    public int Id { get; set; }
    public string ImageUrl { get; set; }
}