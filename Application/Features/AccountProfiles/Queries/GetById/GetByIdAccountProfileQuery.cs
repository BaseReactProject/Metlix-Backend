using Application.Features.AccountProfiles.Constants;
using Application.Features.AccountProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.AccountProfiles.Constants.AccountProfilesOperationClaims;

namespace Application.Features.AccountProfiles.Queries.GetById;

public class GetByIdAccountProfileQuery : IRequest<GetByIdAccountProfileResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdAccountProfileQueryHandler : IRequestHandler<GetByIdAccountProfileQuery, GetByIdAccountProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountProfileRepository _accountProfileRepository;
        private readonly AccountProfileBusinessRules _accountProfileBusinessRules;

        public GetByIdAccountProfileQueryHandler(IMapper mapper, IAccountProfileRepository accountProfileRepository, AccountProfileBusinessRules accountProfileBusinessRules)
        {
            _mapper = mapper;
            _accountProfileRepository = accountProfileRepository;
            _accountProfileBusinessRules = accountProfileBusinessRules;
        }

        public async Task<GetByIdAccountProfileResponse> Handle(GetByIdAccountProfileQuery request, CancellationToken cancellationToken)
        {
            AccountProfile? accountProfile = await _accountProfileRepository.GetAsync(predicate: ap => ap.Id == request.Id, cancellationToken: cancellationToken);
            await _accountProfileBusinessRules.AccountProfileShouldExistWhenSelected(accountProfile);

            GetByIdAccountProfileResponse response = _mapper.Map<GetByIdAccountProfileResponse>(accountProfile);
            return response;
        }
    }
}