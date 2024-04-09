

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Image : Entity<int>
{
    public Image()
    {
        ImageUrl = string.Empty;
    }

    public Image(string ımageUrl)
    {
        ImageUrl = ımageUrl;
    }

    public string ImageUrl { get; set; }
}
