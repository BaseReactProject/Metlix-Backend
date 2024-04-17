using Application.Features.ContentIntroes.Constants;
using Application.Features.ContentIntroes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentIntroes.Constants.ContentIntroesOperationClaims;

namespace Application.Features.ContentIntroes.Commands.Create;

public class CreateContentIntroCommand : IRequest<CreatedContentIntroResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int ContentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentIntroesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentIntroes";

    public class CreateContentIntroCommandHandler : IRequestHandler<CreateContentIntroCommand, CreatedContentIntroResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentIntroRepository _contentIntroRepository;
        private readonly ContentIntroBusinessRules _contentIntroBusinessRules;

        public CreateContentIntroCommandHandler(IMapper mapper, IContentIntroRepository contentIntroRepository,
                                         ContentIntroBusinessRules contentIntroBusinessRules)
        {
            _mapper = mapper;
            _contentIntroRepository = contentIntroRepository;
            _contentIntroBusinessRules = contentIntroBusinessRules;
        }

        public async Task<CreatedContentIntroResponse> Handle(CreateContentIntroCommand request, CancellationToken cancellationToken)
        {
            ContentIntro contentIntro = _mapper.Map<ContentIntro>(request);

            await _contentIntroRepository.AddAsync(contentIntro);

            CreatedContentIntroResponse response = _mapper.Map<CreatedContentIntroResponse>(contentIntro);
            return response;
        }
    }
}