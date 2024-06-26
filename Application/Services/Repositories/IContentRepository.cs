using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentRepository : IAsyncRepository<Content, int>, IRepository<Content, int>
{
}