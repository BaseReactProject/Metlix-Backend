using Application.Features.AccountCreditCards.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.AccountCreditCards.Constants.AccountCreditCardsOperationClaims;

namespace Application.Features.AccountCreditCards.Queries.GetList;

public class GetListAccountCreditCardQuery : IRequest<GetListResponse<GetListAccountCreditCardListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListAccountCreditCards({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAccountCreditCards";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAccountCreditCardQueryHandler : IRequestHandler<GetListAccountCreditCardQuery, GetListResponse<GetListAccountCreditCardListItemDto>>
    {
        private readonly IAccountCreditCardRepository _accountCreditCardRepository;
        private readonly IMapper _mapper;

        public GetListAccountCreditCardQueryHandler(IAccountCreditCardRepository accountCreditCardRepository, IMapper mapper)
        {
            _accountCreditCardRepository = accountCreditCardRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAccountCreditCardListItemDto>> Handle(GetListAccountCreditCardQuery request, CancellationToken cancellationToken)
        {
            IPaginate<AccountCreditCard> accountCreditCards = await _accountCreditCardRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAccountCreditCardListItemDto> response = _mapper.Map<GetListResponse<GetListAccountCreditCardListItemDto>>(accountCreditCards);
            return response;
        }
    }
}