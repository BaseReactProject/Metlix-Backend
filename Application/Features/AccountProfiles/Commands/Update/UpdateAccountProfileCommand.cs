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

namespace Application.Features.AccountProfiles.Commands.Update;

public class UpdateAccountProfileCommand : IRequest<UpdatedAccountProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int ProfileId { get; set; }

    public string[] Roles => new[] { Admin, Write, AccountProfilesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAccountProfiles";

    public class UpdateAccountProfileCommandHandler : IRequestHandler<UpdateAccountProfileCommand, UpdatedAccountProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountProfileRepository _accountProfileRepository;
        private readonly AccountProfileBusinessRules _accountProfileBusinessRules;

        public UpdateAccountProfileCommandHandler(IMapper mapper, IAccountProfileRepository accountProfileRepository,
                                         AccountProfileBusinessRules accountProfileBusinessRules)
        {
            _mapper = mapper;
            _accountProfileRepository = accountProfileRepository;
            _accountProfileBusinessRules = accountProfileBusinessRules;
        }

        public async Task<UpdatedAccountProfileResponse> Handle(UpdateAccountProfileCommand request, CancellationToken cancellationToken)
        {
            AccountProfile? accountProfile = await _accountProfileRepository.GetAsync(predicate: ap => ap.Id == request.Id, cancellationToken: cancellationToken);
            await _accountProfileBusinessRules.AccountProfileShouldExistWhenSelected(accountProfile);
            accountProfile = _mapper.Map(request, accountProfile);

            await _accountProfileRepository.UpdateAsync(accountProfile!);

            UpdatedAccountProfileResponse response = _mapper.Map<UpdatedAccountProfileResponse>(accountProfile);
            return response;
        }
    }
}