using Application.Features.AccountCreditCards.Commands.Create;
using Application.Features.AccountCreditCards.Commands.Delete;
using Application.Features.AccountCreditCards.Commands.Update;
using Application.Features.AccountCreditCards.Queries.GetById;
using Application.Features.AccountCreditCards.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.AccountCreditCards.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<AccountCreditCard, CreateAccountCreditCardCommand>().ReverseMap();
        CreateMap<AccountCreditCard, CreatedAccountCreditCardResponse>().ReverseMap();
        CreateMap<AccountCreditCard, UpdateAccountCreditCardCommand>().ReverseMap();
        CreateMap<AccountCreditCard, UpdatedAccountCreditCardResponse>().ReverseMap();
        CreateMap<AccountCreditCard, DeleteAccountCreditCardCommand>().ReverseMap();
        CreateMap<AccountCreditCard, DeletedAccountCreditCardResponse>().ReverseMap();
        CreateMap<AccountCreditCard, GetByIdAccountCreditCardResponse>().ReverseMap();
        CreateMap<AccountCreditCard, GetListAccountCreditCardListItemDto>().ReverseMap();
        CreateMap<IPaginate<AccountCreditCard>, GetListResponse<GetListAccountCreditCardListItemDto>>().ReverseMap();
    }
}