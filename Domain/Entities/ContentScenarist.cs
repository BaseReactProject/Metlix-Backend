

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContentScenarist : Entity<int>
{
    public ContentScenarist()
    {
        ContentId = 0;
        PersonId = 0;

    }

    public ContentScenarist(int contentId, int personId, int id) : base(id)
    {
        PersonId = personId;
        ContentId = contentId;
    }

    public int ContentId { get; set; }
    public int PersonId { get; set; }
    public virtual Content? Content { get; set; }
    public virtual Person? Person { get; set; }
}
