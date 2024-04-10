using Application.Features.Profiles.Constants;
using Application.Features.Profiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Profiles.Constants.ProfilesOperationClaims;

namespace Application.Features.Profiles.Commands.Create;

public class CreateProfileCommand : IRequest<CreatedProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public CreateProfileCommand()
    {
        Name = string.Empty;
        ImageId = 0;
    }

    public CreateProfileCommand(string name, int ýmageId)
    {
        Name = name;
        ImageId = ýmageId;
    }

    public string Name { get; set; }
    public int ImageId { get; set; }

    public string[] Roles => new[] { Admin, Write, ProfilesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetProfiles";

    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, CreatedProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly ProfileBusinessRules _profileBusinessRules;

        public CreateProfileCommandHandler(IMapper mapper, IProfileRepository profileRepository,
                                         ProfileBusinessRules profileBusinessRules)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _profileBusinessRules = profileBusinessRules;
        }

        public async Task<CreatedProfileResponse> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Profile profile = _mapper.Map<Domain.Entities.Profile>(request);

            await _profileRepository.AddAsync(profile);

            CreatedProfileResponse response = _mapper.Map<CreatedProfileResponse>(profile);
            return response;
        }
    }
}