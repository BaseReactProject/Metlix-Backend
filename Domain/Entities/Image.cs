

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Image : Entity<int>
{
    public Image()
    {
        ImageUrl = string.Empty;
    }

    public Image(string ımageUrl, int id) : base(id)
    {
        ImageUrl = ımageUrl;
    }

    public string ImageUrl { get; set; }
}
