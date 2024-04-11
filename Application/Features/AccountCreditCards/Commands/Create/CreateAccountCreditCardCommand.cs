using Application.Features.AccountCreditCards.Constants;
using Application.Features.AccountCreditCards.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.AccountCreditCards.Constants.AccountCreditCardsOperationClaims;
using Microsoft.AspNetCore.Http;
using Application.Services.Jwt.Extensions;
using Application.Features.Constants;

namespace Application.Features.AccountCreditCards.Commands.Create;

public class CreateAccountCreditCardCommand : IRequest<CreatedAccountCreditCardResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int AccountId { get; set; }

    public string[] Roles => new[] {MetflixOperationClaims.Account };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAccountCreditCards";

    public class CreateAccountCreditCardCommandHandler : IRequestHandler<CreateAccountCreditCardCommand, CreatedAccountCreditCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountCreditCardRepository _accountCreditCardRepository;
        private readonly AccountCreditCardBusinessRules _accountCreditCardBusinessRules;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateAccountCreditCardCommandHandler(IMapper mapper, IAccountCreditCardRepository accountCreditCardRepository,
                                         AccountCreditCardBusinessRules accountCreditCardBusinessRules, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _accountCreditCardRepository = accountCreditCardRepository;
            _accountCreditCardBusinessRules = accountCreditCardBusinessRules;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreatedAccountCreditCardResponse> Handle(CreateAccountCreditCardCommand request, CancellationToken cancellationToken)
        {
            AccountCreditCard accountCreditCard = _mapper.Map<AccountCreditCard>(request);
            accountCreditCard.Id = _httpContextAccessor.HttpContext!.User.GetAccountId();
            await _accountCreditCardRepository.AddAsync(accountCreditCard);

            CreatedAccountCreditCardResponse response = _mapper.Map<CreatedAccountCreditCardResponse>(accountCreditCard);
            return response;
        }
    }
}