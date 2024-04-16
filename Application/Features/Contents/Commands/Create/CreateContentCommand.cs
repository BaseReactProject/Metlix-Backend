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
using Application.Features.OperationClaims.Constants;

namespace Application.Features.Contents.Commands.Create;

public class CreateContentCommand : IRequest<CreatedContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Content Content { get; set; }
    public string[] Roles => new[] { GeneralOperationClaims.Admin };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContents";

    public class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, CreatedContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentRepository _contentRepository;
        private readonly ContentBusinessRules _contentBusinessRules;

        public CreateContentCommandHandler(IMapper mapper, IContentRepository contentRepository,
                                         ContentBusinessRules contentBusinessRules)
        {
            _mapper = mapper;
            _contentRepository = contentRepository;
            _contentBusinessRules = contentBusinessRules;
        }

        public async Task<CreatedContentResponse> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            Content content = _mapper.Map<Content>(request);

            await _contentRepository.AddAsync(content);

            CreatedContentResponse response = _mapper.Map<CreatedContentResponse>(content);
            return response;
        }
    }
}