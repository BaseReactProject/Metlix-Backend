using Application.Features.ContentTrailers.Constants;
using Application.Features.ContentTrailers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentTrailers.Constants.ContentTrailersOperationClaims;

namespace Application.Features.ContentTrailers.Queries.GetById;

public class GetByIdContentTrailerQuery : IRequest<GetByIdContentTrailerResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentTrailerQueryHandler : IRequestHandler<GetByIdContentTrailerQuery, GetByIdContentTrailerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentTrailerRepository _contentTrailerRepository;
        private readonly ContentTrailerBusinessRules _contentTrailerBusinessRules;

        public GetByIdContentTrailerQueryHandler(IMapper mapper, IContentTrailerRepository contentTrailerRepository, ContentTrailerBusinessRules contentTrailerBusinessRules)
        {
            _mapper = mapper;
            _contentTrailerRepository = contentTrailerRepository;
            _contentTrailerBusinessRules = contentTrailerBusinessRules;
        }

        public async Task<GetByIdContentTrailerResponse> Handle(GetByIdContentTrailerQuery request, CancellationToken cancellationToken)
        {
            ContentTrailer? contentTrailer = await _contentTrailerRepository.GetAsync(predicate: ct => ct.Id == request.Id, cancellationToken: cancellationToken);
            await _contentTrailerBusinessRules.ContentTrailerShouldExistWhenSelected(contentTrailer);

            GetByIdContentTrailerResponse response = _mapper.Map<GetByIdContentTrailerResponse>(contentTrailer);
            return response;
        }
    }
}