

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentDirector : Entity<int>
{
    public ContentDirector()
    {
        ContentId = 0;
        PersonId = 0;

    }

    public ContentDirector(int contentId, int personId, int id) : base(id)
    {
        PersonId = personId;
        ContentId = contentId;
    }

    public int ContentId { get; set; }
    public int PersonId { get; set; }
    public virtual Content? Content { get; set; }
    public virtual Person? Person { get; set; }
}
