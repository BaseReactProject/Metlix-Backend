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

namespace Application.Features.AccountProfiles.Commands.Create;

public class CreateAccountProfileCommand : IRequest<CreatedAccountProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int AccountId { get; set; }
    public int ProfileId { get; set; }

    public string[] Roles => new[] { Admin, Write, AccountProfilesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAccountProfiles";

    public class CreateAccountProfileCommandHandler : IRequestHandler<CreateAccountProfileCommand, CreatedAccountProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountProfileRepository _accountProfileRepository;
        private readonly AccountProfileBusinessRules _accountProfileBusinessRules;

        public CreateAccountProfileCommandHandler(IMapper mapper, IAccountProfileRepository accountProfileRepository,
                                         AccountProfileBusinessRules accountProfileBusinessRules)
        {
            _mapper = mapper;
            _accountProfileRepository = accountProfileRepository;
            _accountProfileBusinessRules = accountProfileBusinessRules;
        }

        public async Task<CreatedAccountProfileResponse> Handle(CreateAccountProfileCommand request, CancellationToken cancellationToken)
        {
            AccountProfile accountProfile = _mapper.Map<AccountProfile>(request);

            await _accountProfileRepository.AddAsync(accountProfile);

            CreatedAccountProfileResponse response = _mapper.Map<CreatedAccountProfileResponse>(accountProfile);
            return response;
        }
    }
}