using Application.Features.Notices.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Notices.Rules;

public class NoticeBusinessRules : BaseBusinessRules
{
    private readonly INoticeRepository _noticeRepository;

    public NoticeBusinessRules(INoticeRepository noticeRepository)
    {
        _noticeRepository = noticeRepository;
    }

    public Task NoticeShouldExistWhenSelected(Notice? notice)
    {
        if (notice == null)
            throw new BusinessException(NoticesBusinessMessages.NoticeNotExists);
        return Task.CompletedTask;
    }

    public async Task NoticeIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Notice? notice = await _noticeRepository.GetAsync(
            predicate: n => n.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await NoticeShouldExistWhenSelected(notice);
    }
}