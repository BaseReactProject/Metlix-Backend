using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentNoticeRepository : IAsyncRepository<ContentNotice, int>, IRepository<ContentNotice, int>
{
}