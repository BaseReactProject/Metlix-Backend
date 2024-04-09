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

namespace Application.Features.Plans.Commands.Create;

public class CreatePlanCommand : IRequest<CreatedPlanResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public int QualityId { get; set; }
    public string Description { get; set; }
    public int DeviceCount { get; set; }
    public decimal Price { get; set; }

    public string[] Roles => new[] { Admin, Write, PlansOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPlans";

    public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, CreatedPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPlanRepository _planRepository;
        private readonly PlanBusinessRules _planBusinessRules;

        public CreatePlanCommandHandler(IMapper mapper, IPlanRepository planRepository,
                                         PlanBusinessRules planBusinessRules)
        {
            _mapper = mapper;
            _planRepository = planRepository;
            _planBusinessRules = planBusinessRules;
        }

        public async Task<CreatedPlanResponse> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            Plan plan = _mapper.Map<Plan>(request);

            await _planRepository.AddAsync(plan);

            CreatedPlanResponse response = _mapper.Map<CreatedPlanResponse>(plan);
            return response;
        }
    }
}