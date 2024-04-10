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

namespace Application.Features.Plans.Commands.Update;

public class UpdatePlanCommand : MediatR.IRequest<UpdatedPlanResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public UpdatePlanCommand()
    {
        Name = string.Empty;
        QualityId = 0;
        Description = string.Empty;
        DeviceCount = 0;
        Price = 0;
    }

    public UpdatePlanCommand(int id, string name, int qualityId, string description, int deviceCount, decimal price)
    {
        Id = id;
        Name = name;
        QualityId = qualityId;
        Description = description;
        DeviceCount = deviceCount;
        Price = price;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int QualityId { get; set; }
    public string Description { get; set; }
    public int DeviceCount { get; set; }
    public decimal Price { get; set; }

    public string[] Roles => new[] { Admin, Write, PlansOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPlans";

    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, UpdatedPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPlanRepository _planRepository;
        private readonly PlanBusinessRules _planBusinessRules;

        public UpdatePlanCommandHandler(IMapper mapper, IPlanRepository planRepository,
                                         PlanBusinessRules planBusinessRules)
        {
            _mapper = mapper;
            _planRepository = planRepository;
            _planBusinessRules = planBusinessRules;
        }

        public async Task<UpdatedPlanResponse> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            Plan? plan = await _planRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _planBusinessRules.PlanShouldExistWhenSelected(plan);
            plan = _mapper.Map(request, plan);

            await _planRepository.UpdateAsync(plan!);

            UpdatedPlanResponse response = _mapper.Map<UpdatedPlanResponse>(plan);
            return response;
        }
    }
}