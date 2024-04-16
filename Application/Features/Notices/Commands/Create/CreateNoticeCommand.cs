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

namespace Application.Features.Notices.Commands.Create;

public class CreateNoticeCommand : IRequest<CreatedNoticeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, NoticesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetNotices";

    public class CreateNoticeCommandHandler : IRequestHandler<CreateNoticeCommand, CreatedNoticeResponse>
    {
        private readonly IMapper _mapper;
        private readonly INoticeRepository _noticeRepository;
        private readonly NoticeBusinessRules _noticeBusinessRules;

        public CreateNoticeCommandHandler(IMapper mapper, INoticeRepository noticeRepository,
                                         NoticeBusinessRules noticeBusinessRules)
        {
            _mapper = mapper;
            _noticeRepository = noticeRepository;
            _noticeBusinessRules = noticeBusinessRules;
        }

        public async Task<CreatedNoticeResponse> Handle(CreateNoticeCommand request, CancellationToken cancellationToken)
        {
            Notice notice = _mapper.Map<Notice>(request);

            await _noticeRepository.AddAsync(notice);

            CreatedNoticeResponse response = _mapper.Map<CreatedNoticeResponse>(notice);
            return response;
        }
    }
}