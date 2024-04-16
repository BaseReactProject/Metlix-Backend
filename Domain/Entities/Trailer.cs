

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Trailer : Entity<int>
{
    public Trailer(int id, string trailerUrl) : base(id)
    {
        TrailerUrl = trailerUrl;
    }
    public Trailer()
    {
        TrailerUrl = string.Empty;
    }

    public string TrailerUrl { get; set; }
    public virtual ICollection<TrailerGenre>? TrailerGenres { get; set; }
}
