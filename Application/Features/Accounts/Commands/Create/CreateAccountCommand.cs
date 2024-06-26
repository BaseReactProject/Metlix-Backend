using Application.Features.Accounts.Constants;
using Application.Features.Accounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Accounts.Constants.AccountsOperationClaims;

namespace Application.Features.Accounts.Commands.Create;

public class CreateAccountCommand : IRequest<CreatedAccountResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public CreateAccountCommand()
    {
        FakeId = string.Empty;
        UserId = 0;
    }

    public CreateAccountCommand(string fakeId, int userId)
    {
        FakeId = fakeId;
        UserId = userId;
    }

    public string FakeId { get; set; }
    public int UserId { get; set; }

    public string[] Roles => new[] { Admin, Write, AccountsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAccounts";

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreatedAccountResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly AccountBusinessRules _accountBusinessRules;

        public CreateAccountCommandHandler(IMapper mapper, IAccountRepository accountRepository,
                                         AccountBusinessRules accountBusinessRules)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _accountBusinessRules = accountBusinessRules;
        }

        public async Task<CreatedAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = _mapper.Map<Account>(request);

            await _accountRepository.AddAsync(account);

            CreatedAccountResponse response = _mapper.Map<CreatedAccountResponse>(account);
            return response;
        }
    }
}