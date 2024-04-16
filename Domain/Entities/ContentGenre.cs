

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentGenre : Entity<int>
{
    public ContentGenre()
    {
        ContentId = 0;
        GenreId = 0;

    }

    public ContentGenre(int contentId, int genreId, int id) : base(id)
    {
      
        ContentId = contentId;
        GenreId = genreId;
    }

    public int ContentId { get; set; }
    public int GenreId { get; set; }
    public virtual Content? Content { get; set; }
    public virtual Genre? Genre { get; set; }
}
