using Application.Features.Images.Constants;
using Application.Features.Images.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Images.Constants.ImagesOperationClaims;
using Microsoft.AspNetCore.Http;
using Application.Services.ImageService;

namespace Application.Features.Images.Commands.Create;

public class CreateImageCommand : IRequest<CreatedImageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public CreateImageCommand()
    {
        ImagePath = new FormFile(Stream.Null, 0, 0, string.Empty, string.Empty) ;
    }

    public CreateImageCommand(IFormFile imagePath)
    {
        ImagePath = imagePath;
    }

    public IFormFile ImagePath { get; set; }

    public string[] Roles => new[] { Admin, Write, ImagesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetImages";

    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, CreatedImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly ImageBusinessRules _imageBusinessRules;
        private readonly ImageServiceBase imageServiceBase;

        public CreateImageCommandHandler(IMapper mapper, IImageRepository imageRepository,
                                         ImageBusinessRules imageBusinessRules, ImageServiceBase �mageServiceBase)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _imageBusinessRules = imageBusinessRules;
            imageServiceBase = �mageServiceBase;
        }

        public async Task<CreatedImageResponse> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            Image image = new()
            {
                ImageUrl = await imageServiceBase.UploadAsync(request.ImagePath)
            };
            await _imageRepository.AddAsync(image);

            CreatedImageResponse response = _mapper.Map<CreatedImageResponse>(image);
            return response;
        }
    }
}