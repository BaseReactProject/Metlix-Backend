using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentTrailerRepository : IAsyncRepository<ContentTrailer, int>, IRepository<ContentTrailer, int>
{
}