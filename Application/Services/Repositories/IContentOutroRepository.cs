using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentOutroRepository : IAsyncRepository<ContentOutro, int>, IRepository<ContentOutro, int>
{
}