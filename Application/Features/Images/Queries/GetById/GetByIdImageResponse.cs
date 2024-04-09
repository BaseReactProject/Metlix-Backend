using Core.Application.Responses;

namespace Application.Features.Images.Queries.GetById;

public class GetByIdImageResponse : IResponse
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
}