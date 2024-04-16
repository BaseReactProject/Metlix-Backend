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

namespace Application.Features.Trailers.Commands.Delete;

public class DeleteTrailerCommand : IRequest<DeletedTrailerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, TrailersOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetTrailers";

    public class DeleteTrailerCommandHandler : IRequestHandler<DeleteTrailerCommand, DeletedTrailerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITrailerRepository _trailerRepository;
        private readonly TrailerBusinessRules _trailerBusinessRules;

        public DeleteTrailerCommandHandler(IMapper mapper, ITrailerRepository trailerRepository,
                                         TrailerBusinessRules trailerBusinessRules)
        {
            _mapper = mapper;
            _trailerRepository = trailerRepository;
            _trailerBusinessRules = trailerBusinessRules;
        }

        public async Task<DeletedTrailerResponse> Handle(DeleteTrailerCommand request, CancellationToken cancellationToken)
        {
            Trailer? trailer = await _trailerRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _trailerBusinessRules.TrailerShouldExistWhenSelected(trailer);

            await _trailerRepository.DeleteAsync(trailer!);

            DeletedTrailerResponse response = _mapper.Map<DeletedTrailerResponse>(trailer);
            return response;
        }
    }
}