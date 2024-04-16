using Application.Features.Notices.Constants;
using Application.Features.Notices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Notices.Constants.NoticesOperationClaims;

namespace Application.Features.Notices.Queries.GetById;

public class GetByIdNoticeQuery : IRequest<GetByIdNoticeResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdNoticeQueryHandler : IRequestHandler<GetByIdNoticeQuery, GetByIdNoticeResponse>
    {
        private readonly IMapper _mapper;
        private readonly INoticeRepository _noticeRepository;
        private readonly NoticeBusinessRules _noticeBusinessRules;

        public GetByIdNoticeQueryHandler(IMapper mapper, INoticeRepository noticeRepository, NoticeBusinessRules noticeBusinessRules)
        {
            _mapper = mapper;
            _noticeRepository = noticeRepository;
            _noticeBusinessRules = noticeBusinessRules;
        }

        public async Task<GetByIdNoticeResponse> Handle(GetByIdNoticeQuery request, CancellationToken cancellationToken)
        {
            Notice? notice = await _noticeRepository.GetAsync(predicate: n => n.Id == request.Id, cancellationToken: cancellationToken);
            await _noticeBusinessRules.NoticeShouldExistWhenSelected(notice);

            GetByIdNoticeResponse response = _mapper.Map<GetByIdNoticeResponse>(notice);
            return response;
        }
    }
}