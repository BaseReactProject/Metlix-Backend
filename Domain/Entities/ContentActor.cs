

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentActor : Entity<int>
{
    public ContentActor()
    {
        ContentId = 0;
        PersonId = 0;

    }

    public ContentActor(int contentId, int personId, int id) : base(id)
    {
        PersonId = personId;
        ContentId = contentId;
    }

    public int ContentId { get; set; }
    public int PersonId { get; set; }
    public virtual Content? Content { get; set; }
    public virtual Person? Person { get; set; }
}
