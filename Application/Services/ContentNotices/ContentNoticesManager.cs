using Application.Features.ContentNotices.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentNotices;

public class ContentNoticesManager : IContentNoticesService
{
    private readonly IContentNoticeRepository _contentNoticeRepository;
    private readonly ContentNoticeBusinessRules _contentNoticeBusinessRules;

    public ContentNoticesManager(IContentNoticeRepository contentNoticeRepository, ContentNoticeBusinessRules contentNoticeBusinessRules)
    {
        _contentNoticeRepository = contentNoticeRepository;
        _contentNoticeBusinessRules = contentNoticeBusinessRules;
    }

    public async Task<ContentNotice?> GetAsync(
        Expression<Func<ContentNotice, bool>> predicate,
        Func<IQueryable<ContentNotice>, IIncludableQueryable<ContentNotice, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentNotice? contentNotice = await _contentNoticeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentNotice;
    }

    public async Task<IPaginate<ContentNotice>?> GetListAsync(
        Expression<Func<ContentNotice, bool>>? predicate = null,
        Func<IQueryable<ContentNotice>, IOrderedQueryable<ContentNotice>>? orderBy = null,
        Func<IQueryable<ContentNotice>, IIncludableQueryable<ContentNotice, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentNotice> contentNoticeList = await _contentNoticeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentNoticeList;
    }

    public async Task<ContentNotice> AddAsync(ContentNotice contentNotice)
    {
        ContentNotice addedContentNotice = await _contentNoticeRepository.AddAsync(contentNotice);

        return addedContentNotice;
    }

    public async Task<ContentNotice> UpdateAsync(ContentNotice contentNotice)
    {
        ContentNotice updatedContentNotice = await _contentNoticeRepository.UpdateAsync(contentNotice);

        return updatedContentNotice;
    }

    public async Task<ContentNotice> DeleteAsync(ContentNotice contentNotice, bool permanent = false)
    {
        ContentNotice deletedContentNotice = await _contentNoticeRepository.DeleteAsync(contentNotice);

        return deletedContentNotice;
    }
}
