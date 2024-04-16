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

namespace Application.Features.ContentNotices.Commands.Delete;

public class DeleteContentNoticeCommand : IRequest<DeletedContentNoticeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentNoticesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentNotices";

    public class DeleteContentNoticeCommandHandler : IRequestHandler<DeleteContentNoticeCommand, DeletedContentNoticeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentNoticeRepository _contentNoticeRepository;
        private readonly ContentNoticeBusinessRules _contentNoticeBusinessRules;

        public DeleteContentNoticeCommandHandler(IMapper mapper, IContentNoticeRepository contentNoticeRepository,
                                         ContentNoticeBusinessRules contentNoticeBusinessRules)
        {
            _mapper = mapper;
            _contentNoticeRepository = contentNoticeRepository;
            _contentNoticeBusinessRules = contentNoticeBusinessRules;
        }

        public async Task<DeletedContentNoticeResponse> Handle(DeleteContentNoticeCommand request, CancellationToken cancellationToken)
        {
            ContentNotice? contentNotice = await _contentNoticeRepository.GetAsync(predicate: cn => cn.Id == request.Id, cancellationToken: cancellationToken);
            await _contentNoticeBusinessRules.ContentNoticeShouldExistWhenSelected(contentNotice);

            await _contentNoticeRepository.DeleteAsync(contentNotice!);

            DeletedContentNoticeResponse response = _mapper.Map<DeletedContentNoticeResponse>(contentNotice);
            return response;
        }
    }
}