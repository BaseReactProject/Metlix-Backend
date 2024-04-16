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

namespace Application.Features.ContentDirectors.Commands.Create;

public class CreateContentDirectorCommand : IRequest<CreatedContentDirectorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int ContentId { get; set; }
    public int PersonId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentDirectorsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentDirectors";

    public class CreateContentDirectorCommandHandler : IRequestHandler<CreateContentDirectorCommand, CreatedContentDirectorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentDirectorRepository _contentDirectorRepository;
        private readonly ContentDirectorBusinessRules _contentDirectorBusinessRules;

        public CreateContentDirectorCommandHandler(IMapper mapper, IContentDirectorRepository contentDirectorRepository,
                                         ContentDirectorBusinessRules contentDirectorBusinessRules)
        {
            _mapper = mapper;
            _contentDirectorRepository = contentDirectorRepository;
            _contentDirectorBusinessRules = contentDirectorBusinessRules;
        }

        public async Task<CreatedContentDirectorResponse> Handle(CreateContentDirectorCommand request, CancellationToken cancellationToken)
        {
            ContentDirector contentDirector = _mapper.Map<ContentDirector>(request);

            await _contentDirectorRepository.AddAsync(contentDirector);

            CreatedContentDirectorResponse response = _mapper.Map<CreatedContentDirectorResponse>(contentDirector);
            return response;
        }
    }
}