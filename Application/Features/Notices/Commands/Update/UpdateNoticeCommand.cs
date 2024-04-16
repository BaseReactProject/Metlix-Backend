using Application.Features.Notices.Constants;
using Application.Features.Notices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Notices.Constants.NoticesOperationClaims;

namespace Application.Features.Notices.Commands.Update;

public class UpdateNoticeCommand : IRequest<UpdatedNoticeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, NoticesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetNotices";

    public class UpdateNoticeCommandHandler : IRequestHandler<UpdateNoticeCommand, UpdatedNoticeResponse>
    {
        private readonly IMapper _mapper;
        private readonly INoticeRepository _noticeRepository;
        private readonly NoticeBusinessRules _noticeBusinessRules;

        public UpdateNoticeCommandHandler(IMapper mapper, INoticeRepository noticeRepository,
                                         NoticeBusinessRules noticeBusinessRules)
        {
            _mapper = mapper;
            _noticeRepository = noticeRepository;
            _noticeBusinessRules = noticeBusinessRules;
        }

        public async Task<UpdatedNoticeResponse> Handle(UpdateNoticeCommand request, CancellationToken cancellationToken)
        {
            Notice? notice = await _noticeRepository.GetAsync(predicate: n => n.Id == request.Id, cancellationToken: cancellationToken);
            await _noticeBusinessRules.NoticeShouldExistWhenSelected(notice);
            notice = _mapper.Map(request, notice);

            await _noticeRepository.UpdateAsync(notice!);

            UpdatedNoticeResponse response = _mapper.Map<UpdatedNoticeResponse>(notice);
            return response;
        }
    }
}