using Application.Features.Qualities.Constants;
using Application.Features.Qualities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Qualities.Constants.QualitiesOperationClaims;

namespace Application.Features.Qualities.Queries.GetById;

public class GetByIdQualityQuery : IRequest<GetByIdQualityResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdQualityQueryHandler : IRequestHandler<GetByIdQualityQuery, GetByIdQualityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQualityRepository _qualityRepository;
        private readonly QualityBusinessRules _qualityBusinessRules;

        public GetByIdQualityQueryHandler(IMapper mapper, IQualityRepository qualityRepository, QualityBusinessRules qualityBusinessRules)
        {
            _mapper = mapper;
            _qualityRepository = qualityRepository;
            _qualityBusinessRules = qualityBusinessRules;
        }

        public async Task<GetByIdQualityResponse> Handle(GetByIdQualityQuery request, CancellationToken cancellationToken)
        {
            Quality? quality = await _qualityRepository.GetAsync(predicate: q => q.Id == request.Id, cancellationToken: cancellationToken);
            await _qualityBusinessRules.QualityShouldExistWhenSelected(quality);

            GetByIdQualityResponse response = _mapper.Map<GetByIdQualityResponse>(quality);
            return response;
        }
    }
}