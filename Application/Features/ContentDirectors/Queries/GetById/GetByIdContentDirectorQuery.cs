using Application.Features.ContentDirectors.Constants;
using Application.Features.ContentDirectors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentDirectors.Constants.ContentDirectorsOperationClaims;

namespace Application.Features.ContentDirectors.Queries.GetById;

public class GetByIdContentDirectorQuery : IRequest<GetByIdContentDirectorResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentDirectorQueryHandler : IRequestHandler<GetByIdContentDirectorQuery, GetByIdContentDirectorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentDirectorRepository _contentDirectorRepository;
        private readonly ContentDirectorBusinessRules _contentDirectorBusinessRules;

        public GetByIdContentDirectorQueryHandler(IMapper mapper, IContentDirectorRepository contentDirectorRepository, ContentDirectorBusinessRules contentDirectorBusinessRules)
        {
            _mapper = mapper;
            _contentDirectorRepository = contentDirectorRepository;
            _contentDirectorBusinessRules = contentDirectorBusinessRules;
        }

        public async Task<GetByIdContentDirectorResponse> Handle(GetByIdContentDirectorQuery request, CancellationToken cancellationToken)
        {
            ContentDirector? contentDirector = await _contentDirectorRepository.GetAsync(predicate: cd => cd.Id == request.Id, cancellationToken: cancellationToken);
            await _contentDirectorBusinessRules.ContentDirectorShouldExistWhenSelected(contentDirector);

            GetByIdContentDirectorResponse response = _mapper.Map<GetByIdContentDirectorResponse>(contentDirector);
            return response;
        }
    }
}