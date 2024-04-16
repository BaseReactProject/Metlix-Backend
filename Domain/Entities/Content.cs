

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Content:Entity<int>
{
    public Content()
    {
        Name=string.Empty;
        ThumbnailUrl=string.Empty;
        AgeLimit = string.Empty;
        Description= string.Empty;
        MovieId = 0;
        Duration = 0;
        ReleaseDate = DateTime.MinValue;

    }

    public Content(string name, int movieId, string thumbnailUrl, float duration, DateTime releaseDate, string ageLimit, string description,int id):base(id)
    {
        Name = name;
        MovieId = movieId;
        ThumbnailUrl = thumbnailUrl;
        Duration = duration;
        ReleaseDate = releaseDate;
        AgeLimit = ageLimit;
        Description = description;
    }

    public string Name { get; set; }
    public int MovieId { get; set; }
    public string ThumbnailUrl { get; set; }
    public float Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string AgeLimit { get; set; }
    public string Description { get; set; }
    public virtual Movie? Movie { get; set; }
    public virtual ICollection<ContentNotice>? ContentNotices { get; set; }
    public virtual ICollection<ContentActor>? ContentActors { get; set; }
    public virtual ICollection<ContentDirector>? ContentDirectors { get; set; }
    public virtual ICollection<ContentScenarist>? ContentScenarists { get; set; }
    public virtual ICollection<ContentGenre>? ContentGenres { get; set; }
    public virtual ICollection<ContentCategory>? ContentCategories { get; set; }
    public virtual ICollection<ContentTrailer>? ContentTrailers { get; set; }
}
