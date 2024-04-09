using Core.Application.Responses;

namespace Application.Features.Images.Commands.Create;

public class CreatedImageResponse : IResponse
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
}