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

namespace Application.Features.Qualities.Commands.Delete;

public class DeleteQualityCommand : IRequest<DeletedQualityResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, QualitiesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetQualities";

    public class DeleteQualityCommandHandler : IRequestHandler<DeleteQualityCommand, DeletedQualityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQualityRepository _qualityRepository;
        private readonly QualityBusinessRules _qualityBusinessRules;

        public DeleteQualityCommandHandler(IMapper mapper, IQualityRepository qualityRepository,
                                         QualityBusinessRules qualityBusinessRules)
        {
            _mapper = mapper;
            _qualityRepository = qualityRepository;
            _qualityBusinessRules = qualityBusinessRules;
        }

        public async Task<DeletedQualityResponse> Handle(DeleteQualityCommand request, CancellationToken cancellationToken)
        {
            Quality? quality = await _qualityRepository.GetAsync(predicate: q => q.Id == request.Id, cancellationToken: cancellationToken);
            await _qualityBusinessRules.QualityShouldExistWhenSelected(quality);

            await _qualityRepository.DeleteAsync(quality!);

            DeletedQualityResponse response = _mapper.Map<DeletedQualityResponse>(quality);
            return response;
        }
    }
}