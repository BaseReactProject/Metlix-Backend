using Application.Features.ContentNotices.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentNotices.Rules;

public class ContentNoticeBusinessRules : BaseBusinessRules
{
    private readonly IContentNoticeRepository _contentNoticeRepository;

    public ContentNoticeBusinessRules(IContentNoticeRepository contentNoticeRepository)
    {
        _contentNoticeRepository = contentNoticeRepository;
    }

    public Task ContentNoticeShouldExistWhenSelected(ContentNotice? contentNotice)
    {
        if (contentNotice == null)
            throw new BusinessException(ContentNoticesBusinessMessages.ContentNoticeNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentNoticeIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ContentNotice? contentNotice = await _contentNoticeRepository.GetAsync(
            predicate: cn => cn.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentNoticeShouldExistWhenSelected(contentNotice);
    }
}