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

namespace Application.Features.Notices.Commands.Delete;

public class DeleteNoticeCommand : IRequest<DeletedNoticeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, NoticesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetNotices";

    public class DeleteNoticeCommandHandler : IRequestHandler<DeleteNoticeCommand, DeletedNoticeResponse>
    {
        private readonly IMapper _mapper;
        private readonly INoticeRepository _noticeRepository;
        private readonly NoticeBusinessRules _noticeBusinessRules;

        public DeleteNoticeCommandHandler(IMapper mapper, INoticeRepository noticeRepository,
                                         NoticeBusinessRules noticeBusinessRules)
        {
            _mapper = mapper;
            _noticeRepository = noticeRepository;
            _noticeBusinessRules = noticeBusinessRules;
        }

        public async Task<DeletedNoticeResponse> Handle(DeleteNoticeCommand request, CancellationToken cancellationToken)
        {
            Notice? notice = await _noticeRepository.GetAsync(predicate: n => n.Id == request.Id, cancellationToken: cancellationToken);
            await _noticeBusinessRules.NoticeShouldExistWhenSelected(notice);

            await _noticeRepository.DeleteAsync(notice!);

            DeletedNoticeResponse response = _mapper.Map<DeletedNoticeResponse>(notice);
            return response;
        }
    }
}