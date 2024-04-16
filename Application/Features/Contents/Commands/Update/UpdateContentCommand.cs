using Application.Features.Contents.Constants;
using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Contents.Constants.ContentsOperationClaims;

namespace Application.Features.Contents.Commands.Update;

public class UpdateContentCommand : IRequest<UpdatedContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MovieId { get; set; }
    public string ThumbnailUrl { get; set; }
    public float Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string AgeLimit { get; set; }
    public string Description { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContents";

    public class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, UpdatedContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentRepository _contentRepository;
        private readonly ContentBusinessRules _contentBusinessRules;

        public UpdateContentCommandHandler(IMapper mapper, IContentRepository contentRepository,
                                         ContentBusinessRules contentBusinessRules)
        {
            _mapper = mapper;
            _contentRepository = contentRepository;
            _contentBusinessRules = contentBusinessRules;
        }

        public async Task<UpdatedContentResponse> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            Content? content = await _contentRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _contentBusinessRules.ContentShouldExistWhenSelected(content);
            content = _mapper.Map(request, content);

            await _contentRepository.UpdateAsync(content!);

            UpdatedContentResponse response = _mapper.Map<UpdatedContentResponse>(content);
            return response;
        }
    }
}