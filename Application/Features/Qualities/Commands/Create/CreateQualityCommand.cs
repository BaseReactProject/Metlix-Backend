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

namespace Application.Features.Qualities.Commands.Create;

public class CreateQualityCommand : IRequest<CreatedQualityResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public CreateQualityCommand()
    {
        Name = string.Empty;
        Value = 0;
    }

    public CreateQualityCommand(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; }
    public int Value { get; set; }

    public string[] Roles => new[] { Admin, Write, QualitiesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetQualities";

    public class CreateQualityCommandHandler : IRequestHandler<CreateQualityCommand, CreatedQualityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQualityRepository _qualityRepository;
        private readonly QualityBusinessRules _qualityBusinessRules;

        public CreateQualityCommandHandler(IMapper mapper, IQualityRepository qualityRepository,
                                         QualityBusinessRules qualityBusinessRules)
        {
            _mapper = mapper;
            _qualityRepository = qualityRepository;
            _qualityBusinessRules = qualityBusinessRules;
        }

        public async Task<CreatedQualityResponse> Handle(CreateQualityCommand request, CancellationToken cancellationToken)
        {
            Quality quality = _mapper.Map<Quality>(request);

            await _qualityRepository.AddAsync(quality);

            CreatedQualityResponse response = _mapper.Map<CreatedQualityResponse>(quality);
            return response;
        }
    }
}