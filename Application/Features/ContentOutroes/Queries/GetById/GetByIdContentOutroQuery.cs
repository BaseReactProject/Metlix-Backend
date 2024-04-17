using Application.Features.ContentOutroes.Constants;
using Application.Features.ContentOutroes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentOutroes.Constants.ContentOutroesOperationClaims;

namespace Application.Features.ContentOutroes.Queries.GetById;

public class GetByIdContentOutroQuery : IRequest<GetByIdContentOutroResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentOutroQueryHandler : IRequestHandler<GetByIdContentOutroQuery, GetByIdContentOutroResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentOutroRepository _contentOutroRepository;
        private readonly ContentOutroBusinessRules _contentOutroBusinessRules;

        public GetByIdContentOutroQueryHandler(IMapper mapper, IContentOutroRepository contentOutroRepository, ContentOutroBusinessRules contentOutroBusinessRules)
        {
            _mapper = mapper;
            _contentOutroRepository = contentOutroRepository;
            _contentOutroBusinessRules = contentOutroBusinessRules;
        }

        public async Task<GetByIdContentOutroResponse> Handle(GetByIdContentOutroQuery request, CancellationToken cancellationToken)
        {
            ContentOutro? contentOutro = await _contentOutroRepository.GetAsync(predicate: co => co.Id == request.Id, cancellationToken: cancellationToken);
            await _contentOutroBusinessRules.ContentOutroShouldExistWhenSelected(contentOutro);

            GetByIdContentOutroResponse response = _mapper.Map<GetByIdContentOutroResponse>(contentOutro);
            return response;
        }
    }
}