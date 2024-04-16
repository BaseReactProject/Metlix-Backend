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
using Core.Security.Hashing;
using Application.Features.Constants;
using Microsoft.AspNetCore.Http;
using Application.Services.AccountProfiles;
using Application.Services.Jwt.Extensions;
using Application.Services.Accounts;
using Application.Features.AccountProfiles.Commands.Create;

namespace Application.Features.Profiles.Commands.Create;

public class CreateProfileCommand : IRequest<CreatedProfileResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public CreateProfileCommand()
    {
        Name = string.Empty;
        Password = string.Empty;
    }

    public CreateProfileCommand(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public string Name { get; set; }
    public string Password { get; set; }

    public string[] Roles => new[] { MetflixOperationClaims.Account };



    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, CreatedProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly ProfileBusinessRules _profileBusinessRules;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountProfileRepository _accountProfilesService;
        private readonly IAccountsService _accountsService;
        public CreateProfileCommandHandler(IMapper mapper, IProfileRepository profileRepository,
                                         ProfileBusinessRules profileBusinessRules, IHttpContextAccessor httpContextAccessor, IAccountProfileRepository accountProfilesService, IAccountsService accountsService)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
            _profileBusinessRules = profileBusinessRules;
            _httpContextAccessor = httpContextAccessor;
            _accountProfilesService = accountProfilesService;
            _accountsService = accountsService;
        }

        public async Task<CreatedProfileResponse> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            await _profileBusinessRules.ProfilePasswordLengthControl(request.Password, 4);

            Account? account = await _accountsService.GetAsync(predicate: a => a.UserId == _httpContextAccessor.HttpContext!.User.GetUserId(), cancellationToken: cancellationToken);
            await _profileBusinessRules.AccountProfileCountControl(account!.Id, 5, cancellationToken);
            HashingHelper.CreatePasswordHash(
               request.Password,
               passwordHash: out byte[] passwordHash,
               passwordSalt: out byte[] passwordSalt
           );

            Domain.Entities.Profile profile = _mapper.Map<Domain.Entities.Profile>(request);
            profile.PasswordHash = passwordHash;
            profile.PasswordSalt = passwordSalt;
            profile.ImageId = 1;
            Domain.Entities.Profile createdProfile = await _profileRepository.AddAsync(profile);
            await _accountProfilesService.AddAsync(new AccountProfile() { AccountId = account!.Id, ProfileId = createdProfile.Id });
            CreatedProfileResponse response = _mapper.Map<CreatedProfileResponse>(profile);
            return response;
        }
    }
}