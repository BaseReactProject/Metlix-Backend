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

namespace Application.Features.Profiles.Commands.Update;

public class UpdateProfileCommand : IRequest<UpdatedProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }

    public string[] Roles => new[] { Admin, Write, ProfilesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetProfiles";

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdatedProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly ProfileBusinessRules _profileBusinessRules;

        public UpdateProfileCommandHandler(IMapper mapper, IProfileRepository profileRepository,
                                         ProfileBusinessRules profileBusinessRules)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _profileBusinessRules = profileBusinessRules;
        }

        public async Task<UpdatedProfileResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            Profile? profile = await _profileRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _profileBusinessRules.ProfileShouldExistWhenSelected(profile);
            profile = _mapper.Map(request, profile);

            await _profileRepository.UpdateAsync(profile!);

            UpdatedProfileResponse response = _mapper.Map<UpdatedProfileResponse>(profile);
            return response;
        }
    }
}