using Application.Features.ContentScenarists.Constants;
using Application.Features.ContentScenarists.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentScenarists.Constants.ContentScenaristsOperationClaims;

namespace Application.Features.ContentScenarists.Queries.GetById;

public class GetByIdContentScenaristQuery : IRequest<GetByIdContentScenaristResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentScenaristQueryHandler : IRequestHandler<GetByIdContentScenaristQuery, GetByIdContentScenaristResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentScenaristRepository _contentScenaristRepository;
        private readonly ContentScenaristBusinessRules _contentScenaristBusinessRules;

        public GetByIdContentScenaristQueryHandler(IMapper mapper, IContentScenaristRepository contentScenaristRepository, ContentScenaristBusinessRules contentScenaristBusinessRules)
        {
            _mapper = mapper;
            _contentScenaristRepository = contentScenaristRepository;
            _contentScenaristBusinessRules = contentScenaristBusinessRules;
        }

        public async Task<GetByIdContentScenaristResponse> Handle(GetByIdContentScenaristQuery request, CancellationToken cancellationToken)
        {
            ContentScenarist? contentScenarist = await _contentScenaristRepository.GetAsync(predicate: cs => cs.Id == request.Id, cancellationToken: cancellationToken);
            await _contentScenaristBusinessRules.ContentScenaristShouldExistWhenSelected(contentScenarist);

            GetByIdContentScenaristResponse response = _mapper.Map<GetByIdContentScenaristResponse>(contentScenarist);
            return response;
        }
    }
}