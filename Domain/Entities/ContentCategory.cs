

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentCategory : Entity<int>
{
    public ContentCategory()
    {
        ContentId = 0;
        GenreId = 0;

    }

    public ContentCategory(int contentId, int genreId, int id) : base(id)
    {
        GenreId = genreId;
        ContentId = contentId;
    }

    public int ContentId { get; set; }
    public int GenreId { get; set; }
    public virtual Content? Content { get; set; }
    public virtual Genre? Person { get; set; }
}
