using Application.Features.Trailers.Constants;
using Application.Features.Trailers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Trailers.Constants.TrailersOperationClaims;

namespace Application.Features.Trailers.Commands.Create;

public class CreateTrailerCommand : IRequest<CreatedTrailerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string TrailerUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, TrailersOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetTrailers";

    public class CreateTrailerCommandHandler : IRequestHandler<CreateTrailerCommand, CreatedTrailerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITrailerRepository _trailerRepository;
        private readonly TrailerBusinessRules _trailerBusinessRules;

        public CreateTrailerCommandHandler(IMapper mapper, ITrailerRepository trailerRepository,
                                         TrailerBusinessRules trailerBusinessRules)
        {
            _mapper = mapper;
            _trailerRepository = trailerRepository;
            _trailerBusinessRules = trailerBusinessRules;
        }

        public async Task<CreatedTrailerResponse> Handle(CreateTrailerCommand request, CancellationToken cancellationToken)
        {
            Trailer trailer = _mapper.Map<Trailer>(request);

            await _trailerRepository.AddAsync(trailer);

            CreatedTrailerResponse response = _mapper.Map<CreatedTrailerResponse>(trailer);
            return response;
        }
    }
}