

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentTrailer : Entity<int>
{
    public ContentTrailer()
    {
        TrailerId = 0;
        ContentId = 0;

    }

    public ContentTrailer(int trailerId, int contentId, int id) : base(id)
    {
        ContentId = contentId;
        TrailerId = trailerId;
    }

    public int TrailerId { get; set; }
    public int ContentId { get; set; }
    public virtual Trailer? Trailer { get; set; }
    public virtual Content? Content { get; set; }
}
