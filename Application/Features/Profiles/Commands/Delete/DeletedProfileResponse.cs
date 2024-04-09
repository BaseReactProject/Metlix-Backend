using Core.Application.Responses;

namespace Application.Features.Profiles.Commands.Delete;

public class DeletedProfileResponse : IResponse
{
    public int Id { get; set; }
}