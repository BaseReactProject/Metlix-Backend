using Application.Features.ContentNotices.Constants;
using Application.Features.ContentNotices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentNotices.Constants.ContentNoticesOperationClaims;

namespace Application.Features.ContentNotices.Commands.Create;

public class CreateContentNoticeCommand : IRequest<CreatedContentNoticeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int ContentId { get; set; }
    public int NoticeId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentNoticesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentNotices";

    public class CreateContentNoticeCommandHandler : IRequestHandler<CreateContentNoticeCommand, CreatedContentNoticeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentNoticeRepository _contentNoticeRepository;
        private readonly ContentNoticeBusinessRules _contentNoticeBusinessRules;

        public CreateContentNoticeCommandHandler(IMapper mapper, IContentNoticeRepository contentNoticeRepository,
                                         ContentNoticeBusinessRules contentNoticeBusinessRules)
        {
            _mapper = mapper;
            _contentNoticeRepository = contentNoticeRepository;
            _contentNoticeBusinessRules = contentNoticeBusinessRules;
        }

        public async Task<CreatedContentNoticeResponse> Handle(CreateContentNoticeCommand request, CancellationToken cancellationToken)
        {
            ContentNotice contentNotice = _mapper.Map<ContentNotice>(request);

            await _contentNoticeRepository.AddAsync(contentNotice);

            CreatedContentNoticeResponse response = _mapper.Map<CreatedContentNoticeResponse>(contentNotice);
            return response;
        }
    }
}