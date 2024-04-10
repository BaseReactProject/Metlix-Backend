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

namespace Application.Features.AccountProfiles.Commands.Delete;

public class DeleteAccountProfileCommand : IRequest<DeletedAccountProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, AccountProfilesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAccountProfiles";

    public class DeleteAccountProfileCommandHandler : IRequestHandler<DeleteAccountProfileCommand, DeletedAccountProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountProfileRepository _accountProfileRepository;
        private readonly AccountProfileBusinessRules _accountProfileBusinessRules;

        public DeleteAccountProfileCommandHandler(IMapper mapper, IAccountProfileRepository accountProfileRepository,
                                         AccountProfileBusinessRules accountProfileBusinessRules)
        {
            _mapper = mapper;
            _accountProfileRepository = accountProfileRepository;
            _accountProfileBusinessRules = accountProfileBusinessRules;
        }

        public async Task<DeletedAccountProfileResponse> Handle(DeleteAccountProfileCommand request, CancellationToken cancellationToken)
        {
            AccountProfile? accountProfile = await _accountProfileRepository.GetAsync(predicate: ap => ap.Id == request.Id, cancellationToken: cancellationToken);
            await _accountProfileBusinessRules.AccountProfileShouldExistWhenSelected(accountProfile);

            await _accountProfileRepository.DeleteAsync(accountProfile!);

            DeletedAccountProfileResponse response = _mapper.Map<DeletedAccountProfileResponse>(accountProfile);
            return response;
        }
    }
}