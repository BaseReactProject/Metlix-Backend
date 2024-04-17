using Application.Features.Contents.Constants;
using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using Application.Features.OperationClaims.Constants;
using Microsoft.AspNetCore.Http;
using Application.Services.ImageService;

namespace Application.Features.Contents.Commands.Create;

public class CreateContentCommand : IRequest<CreatedContentResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public int MovieId { get; set; }
    public IFormFile ThumbnailUrl { get; set; }
    public float Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string AgeLimit { get; set; }
    public string Description { get; set; }
    public int? ContentIntroId { get; set; }
    public int? ContentOutroId { get; set; }
    public CreateContentCommand()
    {
        Name = string.Empty;
        ThumbnailUrl = new FormFile(Stream.Null, 0, 0, "", "");
        AgeLimit = string.Empty;
        Description = string.Empty;
        MovieId = 0;
        Duration = 0;
        ReleaseDate = DateTime.MinValue;
        ContentIntroId = 0;
        ContentOutroId = 0;
    }

    public CreateContentCommand(string name, int movieId, IFormFile thumbnailUrl, float duration, DateTime releaseDate, string ageLimit, string description,int contentIntroId, int contentOutroId)
    {
        Name = name;
        MovieId = movieId;
        ThumbnailUrl = thumbnailUrl;
        Duration = duration;
        ReleaseDate = releaseDate;
        AgeLimit = ageLimit;
        Description = description;
        ContentIntroId = contentIntroId;
        ContentOutroId = contentOutroId;
    }
    public string[] Roles => new[] { GeneralOperationClaims.Admin };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContents";

    public class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, CreatedContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentRepository _contentRepository;
        private readonly ContentBusinessRules _contentBusinessRules;
        private readonly ImageServiceBase ýmageServiceBase;

        public CreateContentCommandHandler(IMapper mapper, IContentRepository contentRepository,
                                         ContentBusinessRules contentBusinessRules, ImageServiceBase ýmageServiceBase)
        {
            _mapper = mapper;
            _contentRepository = contentRepository;
            _contentBusinessRules = contentBusinessRules;
            this.ýmageServiceBase = ýmageServiceBase;
        }

        public async Task<CreatedContentResponse> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            Content content = _mapper.Map<Content>(request);
            content.ThumbnailUrl =await ýmageServiceBase.UploadAsync(request.ThumbnailUrl);
            await _contentRepository.AddAsync(content);

            CreatedContentResponse response = _mapper.Map<CreatedContentResponse>(content);
            return response;
        }
    }
}