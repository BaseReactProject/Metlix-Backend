
using Application.Features.AccountProfiles.Queries.GetList;
using Application.Features.Accounts.Queries.GetList;
using Application.Features.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Queries.GetProfiles;

public class GetListProfileForActiveUserQuery : IRequest<GetListResponse<GetListAccountListItemDto>>, ISecuredRequest
{

    public string[] Roles => new[] { MetflixOperationClaims.Account };
    public class GetListProfileForActiveUserQueryHandler : IRequestHandler<GetListProfileForActiveUserQuery, GetListResponse<GetListAccountListItemDto>>
    {
        private readonly IAccountProfileRepository _accountProfilesService;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetListProfileForActiveUserQueryHandler(IAccountProfileRepository accountProfiles, IMapper mapper, IHttpContextAccessor httpContextAccessor, IAccountRepository accountsService)
        {
            _accountProfilesService = accountProfiles;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _accountsService = accountsService;
        }

        public async Task<GetListResponse<GetListAccountListItemDto>> Handle(GetListProfileForActiveUserQuery request, CancellationToken cancellationToken)
        {
            Account? account = await _accountsService.GetAsync(predicate: a => a.UserId == _httpContextAccessor.HttpContext!.User.GetUserId(),cancellationToken:cancellationToken);
            IPaginate<Account> accountProfiles = await _accountsService.GetListAsync(
                predicate:ap=>ap.Id==account!.Id,
                include:ap=>ap.Include(ap => ap.AccountProfiles)
                             .ThenInclude(ap=>ap.Profile)
                             .ThenInclude(p=>p.Image),
                index: 0,
                size: 4,
                cancellationToken: cancellationToken
            );
    

            GetListResponse<GetListAccountListItemDto> response = _mapper.Map<GetListResponse<GetListAccountListItemDto>>(accountProfiles);
            return response;
        }
    }
}