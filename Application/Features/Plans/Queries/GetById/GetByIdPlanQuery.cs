using Application.Features.Plans.Constants;
using Application.Features.Plans.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Plans.Constants.PlansOperationClaims;

namespace Application.Features.Plans.Queries.GetById;

public class GetByIdPlanQuery : IRequest<GetByIdPlanResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdPlanQueryHandler : IRequestHandler<GetByIdPlanQuery, GetByIdPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPlanRepository _planRepository;
        private readonly PlanBusinessRules _planBusinessRules;

        public GetByIdPlanQueryHandler(IMapper mapper, IPlanRepository planRepository, PlanBusinessRules planBusinessRules)
        {
            _mapper = mapper;
            _planRepository = planRepository;
            _planBusinessRules = planBusinessRules;
        }

        public async Task<GetByIdPlanResponse> Handle(GetByIdPlanQuery request, CancellationToken cancellationToken)
        {
            Plan? plan = await _planRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _planBusinessRules.PlanShouldExistWhenSelected(plan);

            GetByIdPlanResponse response = _mapper.Map<GetByIdPlanResponse>(plan);
            return response;
        }
    }
}