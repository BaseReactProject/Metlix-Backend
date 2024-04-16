using Application.Features.ContentNotices.Constants;
using Application.Features.ContentNotices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentNotices.Constants.ContentNoticesOperationClaims;

namespace Application.Features.ContentNotices.Queries.GetById;

public class GetByIdContentNoticeQuery : IRequest<GetByIdContentNoticeResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentNoticeQueryHandler : IRequestHandler<GetByIdContentNoticeQuery, GetByIdContentNoticeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentNoticeRepository _contentNoticeRepository;
        private readonly ContentNoticeBusinessRules _contentNoticeBusinessRules;

        public GetByIdContentNoticeQueryHandler(IMapper mapper, IContentNoticeRepository contentNoticeRepository, ContentNoticeBusinessRules contentNoticeBusinessRules)
        {
            _mapper = mapper;
            _contentNoticeRepository = contentNoticeRepository;
            _contentNoticeBusinessRules = contentNoticeBusinessRules;
        }

        public async Task<GetByIdContentNoticeResponse> Handle(GetByIdContentNoticeQuery request, CancellationToken cancellationToken)
        {
            ContentNotice? contentNotice = await _contentNoticeRepository.GetAsync(predicate: cn => cn.Id == request.Id, cancellationToken: cancellationToken);
            await _contentNoticeBusinessRules.ContentNoticeShouldExistWhenSelected(contentNotice);

            GetByIdContentNoticeResponse response = _mapper.Map<GetByIdContentNoticeResponse>(contentNotice);
            return response;
        }
    }
}