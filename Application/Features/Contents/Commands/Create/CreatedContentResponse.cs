using Core.Application.Responses;

namespace Application.Features.Contents.Commands.Create;

public class CreatedContentResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MovieId { get; set; }
    public string ThumbnailUrl { get; set; }
    public float Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string AgeLimit { get; set; }
    public string Description { get; set; }
}