using Core.Application.Responses;

namespace Application.Features.Profiles.Queries.GetById;

public class GetByIdProfileResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
}