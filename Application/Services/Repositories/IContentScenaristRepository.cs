using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentScenaristRepository : IAsyncRepository<ContentScenarist, int>, IRepository<ContentScenarist, int>
{
}