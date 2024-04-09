using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IQualityRepository : IAsyncRepository<Quality, int>, IRepository<Quality, int>
{
}