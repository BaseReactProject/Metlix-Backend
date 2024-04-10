using Application.Features.Plans.Constants;
using Application.Features.Plans.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Plans.Constants.PlansOperationClaims;

namespace Application.Features.Plans.Commands.Delete;

public class DeletePlanCommand : IRequest<DeletedPlanResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, PlansOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPlans";

    public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, DeletedPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPlanRepository _planRepository;
        private readonly PlanBusinessRules _planBusinessRules;

        public DeletePlanCommandHandler(IMapper mapper, IPlanRepository planRepository,
                                         PlanBusinessRules planBusinessRules)
        {
            _mapper = mapper;
            _planRepository = planRepository;
            _planBusinessRules = planBusinessRules;
        }

        public async Task<DeletedPlanResponse> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            Plan? plan = await _planRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _planBusinessRules.PlanShouldExistWhenSelected(plan);

            await _planRepository.DeleteAsync(plan!);

            DeletedPlanResponse response = _mapper.Map<DeletedPlanResponse>(plan);
            return response;
        }
    }
}