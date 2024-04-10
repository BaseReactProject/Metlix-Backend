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

namespace Application.Features.Images.Commands.Update;

public class UpdateImageCommand : IRequest<UpdatedImageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public UpdateImageCommand()
    {
        ImageUrl = string.Empty;
    }

    public UpdateImageCommand(int ýd, string ýmageUrl)
    {
        Id = ýd;
        ImageUrl = ýmageUrl;
    }

    public int Id { get; set; }
    public string ImageUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, ImagesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetImages";

    public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, UpdatedImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly ImageBusinessRules _imageBusinessRules;

        public UpdateImageCommandHandler(IMapper mapper, IImageRepository imageRepository,
                                         ImageBusinessRules imageBusinessRules)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _imageBusinessRules = imageBusinessRules;
        }

        public async Task<UpdatedImageResponse> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {
            Image? image = await _imageRepository.GetAsync(predicate: i => i.Id == request.Id, cancellationToken: cancellationToken);
            await _imageBusinessRules.ImageShouldExistWhenSelected(image);
            image = _mapper.Map(request, image);

            await _imageRepository.UpdateAsync(image!);

            UpdatedImageResponse response = _mapper.Map<UpdatedImageResponse>(image);
            return response;
        }
    }
}