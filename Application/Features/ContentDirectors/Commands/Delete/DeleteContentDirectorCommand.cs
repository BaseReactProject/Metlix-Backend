using Application.Features.ContentDirectors.Constants;
using Application.Features.ContentDirectors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentDirectors.Constants.ContentDirectorsOperationClaims;

namespace Application.Features.ContentDirectors.Commands.Delete;

public class DeleteContentDirectorCommand : IRequest<DeletedContentDirectorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentDirectorsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentDirectors";

    public class DeleteContentDirectorCommandHandler : IRequestHandler<DeleteContentDirectorCommand, DeletedContentDirectorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentDirectorRepository _contentDirectorRepository;
        private readonly ContentDirectorBusinessRules _contentDirectorBusinessRules;

        public DeleteContentDirectorCommandHandler(IMapper mapper, IContentDirectorRepository contentDirectorRepository,
                                         ContentDirectorBusinessRules contentDirectorBusinessRules)
        {
            _mapper = mapper;
            _contentDirectorRepository = contentDirectorRepository;
            _contentDirectorBusinessRules = contentDirectorBusinessRules;
        }

        public async Task<DeletedContentDirectorResponse> Handle(DeleteContentDirectorCommand request, CancellationToken cancellationToken)
        {
            ContentDirector? contentDirector = await _contentDirectorRepository.GetAsync(predicate: cd => cd.Id == request.Id, cancellationToken: cancellationToken);
            await _contentDirectorBusinessRules.ContentDirectorShouldExistWhenSelected(contentDirector);

            await _contentDirectorRepository.DeleteAsync(contentDirector!);

            DeletedContentDirectorResponse response = _mapper.Map<DeletedContentDirectorResponse>(contentDirector);
            return response;
        }
    }
}