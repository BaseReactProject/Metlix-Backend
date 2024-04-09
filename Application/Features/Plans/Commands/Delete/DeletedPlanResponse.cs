using Core.Application.Responses;

namespace Application.Features.Plans.Commands.Delete;

public class DeletedPlanResponse : IResponse
{
    public int Id { get; set; }
}