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

namespace Application.Features.ContentDirectors.Commands.Update;

public class UpdateContentDirectorCommand : IRequest<UpdatedContentDirectorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentDirectorsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentDirectors";

    public class UpdateContentDirectorCommandHandler : IRequestHandler<UpdateContentDirectorCommand, UpdatedContentDirectorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentDirectorRepository _contentDirectorRepository;
        private readonly ContentDirectorBusinessRules _contentDirectorBusinessRules;

        public UpdateContentDirectorCommandHandler(IMapper mapper, IContentDirectorRepository contentDirectorRepository,
                                         ContentDirectorBusinessRules contentDirectorBusinessRules)
        {
            _mapper = mapper;
            _contentDirectorRepository = contentDirectorRepository;
            _contentDirectorBusinessRules = contentDirectorBusinessRules;
        }

        public async Task<UpdatedContentDirectorResponse> Handle(UpdateContentDirectorCommand request, CancellationToken cancellationToken)
        {
            ContentDirector? contentDirector = await _contentDirectorRepository.GetAsync(predicate: cd => cd.Id == request.Id, cancellationToken: cancellationToken);
            await _contentDirectorBusinessRules.ContentDirectorShouldExistWhenSelected(contentDirector);
            contentDirector = _mapper.Map(request, contentDirector);

            await _contentDirectorRepository.UpdateAsync(contentDirector!);

            UpdatedContentDirectorResponse response = _mapper.Map<UpdatedContentDirectorResponse>(contentDirector);
            return response;
        }
    }
}