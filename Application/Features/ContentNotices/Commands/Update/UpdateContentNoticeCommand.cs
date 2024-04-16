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

namespace Application.Features.ContentNotices.Commands.Update;

public class UpdateContentNoticeCommand : IRequest<UpdatedContentNoticeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int NoticeId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentNoticesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentNotices";

    public class UpdateContentNoticeCommandHandler : IRequestHandler<UpdateContentNoticeCommand, UpdatedContentNoticeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentNoticeRepository _contentNoticeRepository;
        private readonly ContentNoticeBusinessRules _contentNoticeBusinessRules;

        public UpdateContentNoticeCommandHandler(IMapper mapper, IContentNoticeRepository contentNoticeRepository,
                                         ContentNoticeBusinessRules contentNoticeBusinessRules)
        {
            _mapper = mapper;
            _contentNoticeRepository = contentNoticeRepository;
            _contentNoticeBusinessRules = contentNoticeBusinessRules;
        }

        public async Task<UpdatedContentNoticeResponse> Handle(UpdateContentNoticeCommand request, CancellationToken cancellationToken)
        {
            ContentNotice? contentNotice = await _contentNoticeRepository.GetAsync(predicate: cn => cn.Id == request.Id, cancellationToken: cancellationToken);
            await _contentNoticeBusinessRules.ContentNoticeShouldExistWhenSelected(contentNotice);
            contentNotice = _mapper.Map(request, contentNotice);

            await _contentNoticeRepository.UpdateAsync(contentNotice!);

            UpdatedContentNoticeResponse response = _mapper.Map<UpdatedContentNoticeResponse>(contentNotice);
            return response;
        }
    }
}