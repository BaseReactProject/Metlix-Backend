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

namespace Application.Features.Trailers.Commands.Update;

public class UpdateTrailerCommand : IRequest<UpdatedTrailerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string TrailerUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, TrailersOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetTrailers";

    public class UpdateTrailerCommandHandler : IRequestHandler<UpdateTrailerCommand, UpdatedTrailerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITrailerRepository _trailerRepository;
        private readonly TrailerBusinessRules _trailerBusinessRules;

        public UpdateTrailerCommandHandler(IMapper mapper, ITrailerRepository trailerRepository,
                                         TrailerBusinessRules trailerBusinessRules)
        {
            _mapper = mapper;
            _trailerRepository = trailerRepository;
            _trailerBusinessRules = trailerBusinessRules;
        }

        public async Task<UpdatedTrailerResponse> Handle(UpdateTrailerCommand request, CancellationToken cancellationToken)
        {
            Trailer? trailer = await _trailerRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _trailerBusinessRules.TrailerShouldExistWhenSelected(trailer);
            trailer = _mapper.Map(request, trailer);

            await _trailerRepository.UpdateAsync(trailer!);

            UpdatedTrailerResponse response = _mapper.Map<UpdatedTrailerResponse>(trailer);
            return response;
        }
    }
}