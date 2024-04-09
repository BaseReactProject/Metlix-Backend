using Core.Application.Responses;

namespace Application.Features.AccountProfiles.Commands.Delete;

public class DeletedAccountProfileResponse : IResponse
{
    public int Id { get; set; }
}