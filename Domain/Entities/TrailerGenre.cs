

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class TrailerGenre : Entity<int>
{
    public TrailerGenre()
    {
        TrailerId = 0;
        GenreId = 0;

    }

    public TrailerGenre(int trailerId, int genreId, int id) : base(id)
    {
        GenreId = genreId;
        TrailerId = trailerId;
    }

    public int TrailerId { get; set; }
    public int GenreId { get; set; }
    public virtual Trailer? Trailer { get; set; }
    public virtual Genre? Genre { get; set; }
}
