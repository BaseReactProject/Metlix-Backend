using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface INoticeRepository : IAsyncRepository<Notice, int>, IRepository<Notice, int>
{
}