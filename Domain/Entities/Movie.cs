

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Movie : Entity<int>
{
    public Movie(int id,string movieUrl) : base(id)
    {
        MovieUrl = movieUrl;
    }
    public Movie() {
        MovieUrl = string.Empty;
    }

    public string MovieUrl { get; set; }
}
