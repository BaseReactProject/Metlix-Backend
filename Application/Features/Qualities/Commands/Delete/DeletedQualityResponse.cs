using Core.Application.Responses;

namespace Application.Features.Qualities.Commands.Delete;

public class DeletedQualityResponse : IResponse
{
    public int Id { get; set; }
}