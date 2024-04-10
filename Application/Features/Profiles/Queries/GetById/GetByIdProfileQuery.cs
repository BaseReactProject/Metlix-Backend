using Application.Features.Profiles.Constants;
using Application.Features.Profiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Profiles.Constants.ProfilesOperationClaims;

namespace Application.Features.Profiles.Queries.GetById;

public class GetByIdProfileQuery : IRequest<GetByIdProfileResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdProfileQueryHandler : IRequestHandler<GetByIdProfileQuery, GetByIdProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly ProfileBusinessRules _profileBusinessRules;

        public GetByIdProfileQueryHandler(IMapper mapper, IProfileRepository profileRepository, ProfileBusinessRules profileBusinessRules)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _profileBusinessRules = profileBusinessRules;
        }

        public async Task<GetByIdProfileResponse> Handle(GetByIdProfileQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Profile? profile = await _profileRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _profileBusinessRules.ProfileShouldExistWhenSelected(profile);

            GetByIdProfileResponse response = _mapper.Map<GetByIdProfileResponse>(profile);
            return response;
        }
    }
}