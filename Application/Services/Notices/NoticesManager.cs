using Application.Features.Notices.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Notices;

public class NoticesManager : INoticesService
{
    private readonly INoticeRepository _noticeRepository;
    private readonly NoticeBusinessRules _noticeBusinessRules;

    public NoticesManager(INoticeRepository noticeRepository, NoticeBusinessRules noticeBusinessRules)
    {
        _noticeRepository = noticeRepository;
        _noticeBusinessRules = noticeBusinessRules;
    }

    public async Task<Notice?> GetAsync(
        Expression<Func<Notice, bool>> predicate,
        Func<IQueryable<Notice>, IIncludableQueryable<Notice, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Notice? notice = await _noticeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return notice;
    }

    public async Task<IPaginate<Notice>?> GetListAsync(
        Expression<Func<Notice, bool>>? predicate = null,
        Func<IQueryable<Notice>, IOrderedQueryable<Notice>>? orderBy = null,
        Func<IQueryable<Notice>, IIncludableQueryable<Notice, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Notice> noticeList = await _noticeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return noticeList;
    }

    public async Task<Notice> AddAsync(Notice notice)
    {
        Notice addedNotice = await _noticeRepository.AddAsync(notice);

        return addedNotice;
    }

    public async Task<Notice> UpdateAsync(Notice notice)
    {
        Notice updatedNotice = await _noticeRepository.UpdateAsync(notice);

        return updatedNotice;
    }

    public async Task<Notice> DeleteAsync(Notice notice, bool permanent = false)
    {
        Notice deletedNotice = await _noticeRepository.DeleteAsync(notice);

        return deletedNotice;
    }
}
