using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentIntroRepository : IAsyncRepository<ContentIntro, int>, IRepository<ContentIntro, int>
{
}