using Application.Features.AccountProfiles.Constants;
using Application.Features.AccountProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.AccountProfiles.Constants.AccountProfilesOperationClaims;
using Application.Services.Accounts;
using Microsoft.AspNetCore.Http;
using Core.Security.Extensions;
using Application.Features.Constants;

namespace Application.Features.AccountProfiles.Commands.Create;

public class CreateAccountProfileCommand : IRequest<CreatedAccountProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int? AccountId { get; set; }
    public int ProfileId { get; set; }

    public string[] Roles => new[] { MetflixOperationClaims.Account };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAccountProfiles";

    public class CreateAccountProfileCommandHandler : IRequestHandler<CreateAccountProfileCommand, CreatedAccountProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountProfileRepository _accountProfileRepository;
        private readonly AccountProfileBusinessRules _accountProfileBusinessRules;
        private readonly IAccountsService _accountsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateAccountProfileCommandHandler(IMapper mapper, IAccountProfileRepository accountProfileRepository,
                                         AccountProfileBusinessRules accountProfileBusinessRules, IHttpContextAccessor httpContextAccessor, IAccountsService accountsService)
        {
            _mapper = mapper;
            _accountProfileRepository = accountProfileRepository;
            _accountProfileBusinessRules = accountProfileBusinessRules;
            _httpContextAccessor = httpContextAccessor;
            _accountsService = accountsService;
        }

        public async Task<CreatedAccountProfileResponse> Handle(CreateAccountProfileCommand request, CancellationToken cancellationToken)
        {
            Account? account = await _accountsService.GetAsync(predicate: a => a.UserId == _httpContextAccessor.HttpContext!.User.GetUserId(), cancellationToken: cancellationToken);
            request.AccountId = account!.Id;
            AccountProfile accountProfile = _mapper.Map<AccountProfile>(request);
            AccountProfile accountProfile1 = new() { AccountId = account!.Id, ProfileId = request.ProfileId };
            await _accountProfileRepository.AddAsync(accountProfile1);

            CreatedAccountProfileResponse response = _mapper.Map<CreatedAccountProfileResponse>(accountProfile);
            return response;
        }
    }
}