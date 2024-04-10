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

namespace Application.Features.Profiles.Commands.Delete;

public class DeleteProfileCommand : IRequest<DeletedProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ProfilesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetProfiles";

    public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, DeletedProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly ProfileBusinessRules _profileBusinessRules;

        public DeleteProfileCommandHandler(IMapper mapper, IProfileRepository profileRepository,
                                         ProfileBusinessRules profileBusinessRules)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _profileBusinessRules = profileBusinessRules;
        }

        public async Task<DeletedProfileResponse> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Profile? profile = await _profileRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _profileBusinessRules.ProfileShouldExistWhenSelected(profile);

            await _profileRepository.DeleteAsync(profile!);

            DeletedProfileResponse response = _mapper.Map<DeletedProfileResponse>(profile);
            return response;
        }
    }
}