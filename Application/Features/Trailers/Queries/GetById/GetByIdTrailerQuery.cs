using Application.Features.Trailers.Constants;
using Application.Features.Trailers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Trailers.Constants.TrailersOperationClaims;

namespace Application.Features.Trailers.Queries.GetById;

public class GetByIdTrailerQuery : IRequest<GetByIdTrailerResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdTrailerQueryHandler : IRequestHandler<GetByIdTrailerQuery, GetByIdTrailerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITrailerRepository _trailerRepository;
        private readonly TrailerBusinessRules _trailerBusinessRules;

        public GetByIdTrailerQueryHandler(IMapper mapper, ITrailerRepository trailerRepository, TrailerBusinessRules trailerBusinessRules)
        {
            _mapper = mapper;
            _trailerRepository = trailerRepository;
            _trailerBusinessRules = trailerBusinessRules;
        }

        public async Task<GetByIdTrailerResponse> Handle(GetByIdTrailerQuery request, CancellationToken cancellationToken)
        {
            Trailer? trailer = await _trailerRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _trailerBusinessRules.TrailerShouldExistWhenSelected(trailer);

            GetByIdTrailerResponse response = _mapper.Map<GetByIdTrailerResponse>(trailer);
            return response;
        }
    }
}