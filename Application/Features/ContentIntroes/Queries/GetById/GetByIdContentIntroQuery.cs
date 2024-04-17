using Application.Features.ContentIntroes.Constants;
using Application.Features.ContentIntroes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentIntroes.Constants.ContentIntroesOperationClaims;

namespace Application.Features.ContentIntroes.Queries.GetById;

public class GetByIdContentIntroQuery : IRequest<GetByIdContentIntroResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentIntroQueryHandler : IRequestHandler<GetByIdContentIntroQuery, GetByIdContentIntroResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentIntroRepository _contentIntroRepository;
        private readonly ContentIntroBusinessRules _contentIntroBusinessRules;

        public GetByIdContentIntroQueryHandler(IMapper mapper, IContentIntroRepository contentIntroRepository, ContentIntroBusinessRules contentIntroBusinessRules)
        {
            _mapper = mapper;
            _contentIntroRepository = contentIntroRepository;
            _contentIntroBusinessRules = contentIntroBusinessRules;
        }

        public async Task<GetByIdContentIntroResponse> Handle(GetByIdContentIntroQuery request, CancellationToken cancellationToken)
        {
            ContentIntro? contentIntro = await _contentIntroRepository.GetAsync(predicate: ci => ci.Id == request.Id, cancellationToken: cancellationToken);
            await _contentIntroBusinessRules.ContentIntroShouldExistWhenSelected(contentIntro);

            GetByIdContentIntroResponse response = _mapper.Map<GetByIdContentIntroResponse>(contentIntro);
            return response;
        }
    }
}