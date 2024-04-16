using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentNoticeRepository : EfRepositoryBase<ContentNotice, int, BaseDbContext>, IContentNoticeRepository
{
    public ContentNoticeRepository(BaseDbContext context) : base(context)
    {
    }
}