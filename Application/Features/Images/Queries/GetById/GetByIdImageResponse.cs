using Core.Application.Responses;

namespace Application.Features.Images.Queries.GetById;

public class GetByIdImageResponse : IResponse
{
    public GetByIdImageResponse()
    {
        ImageUrl = string.Empty;
    }

    public GetByIdImageResponse(int ýd, string ýmageUrl)
    {
        Id = ýd;
        ImageUrl = ýmageUrl;
    }

    public int Id { get; set; }
    public string ImageUrl { get; set; }
}