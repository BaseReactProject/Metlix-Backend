using Application.Features.Qualities.Constants;
using Application.Features.Qualities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Qualities.Constants.QualitiesOperationClaims;

namespace Application.Features.Qualities.Commands.Update;

public class UpdateQualityCommand : IRequest<UpdatedQualityResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public UpdateQualityCommand()
    {
        Name = string.Empty;
        Value = 0;
    }

    public UpdateQualityCommand(int ýd, string name, int value)
    {
        Id = ýd;
        Name = name;
        Value = value;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }

    public string[] Roles => new[] { Admin, Write, QualitiesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetQualities";

    public class UpdateQualityCommandHandler : IRequestHandler<UpdateQualityCommand, UpdatedQualityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQualityRepository _qualityRepository;
        private readonly QualityBusinessRules _qualityBusinessRules;

        public UpdateQualityCommandHandler(IMapper mapper, IQualityRepository qualityRepository,
                                         QualityBusinessRules qualityBusinessRules)
        {
            _mapper = mapper;
            _qualityRepository = qualityRepository;
            _qualityBusinessRules = qualityBusinessRules;
        }

        public async Task<UpdatedQualityResponse> Handle(UpdateQualityCommand request, CancellationToken cancellationToken)
        {
            Quality? quality = await _qualityRepository.GetAsync(predicate: q => q.Id == request.Id, cancellationToken: cancellationToken);
            await _qualityBusinessRules.QualityShouldExistWhenSelected(quality);
            quality = _mapper.Map(request, quality);

            await _qualityRepository.UpdateAsync(quality!);

            UpdatedQualityResponse response = _mapper.Map<UpdatedQualityResponse>(quality);
            return response;
        }
    }
}