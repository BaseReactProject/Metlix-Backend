using Application.Features.Contents.Constants;
using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Contents.Constants.ContentsOperationClaims;

namespace Application.Features.Contents.Queries.GetById;

public class GetByIdContentQuery : IRequest<GetByIdContentResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentQueryHandler : IRequestHandler<GetByIdContentQuery, GetByIdContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentRepository _contentRepository;
        private readonly ContentBusinessRules _contentBusinessRules;

        public GetByIdContentQueryHandler(IMapper mapper, IContentRepository contentRepository, ContentBusinessRules contentBusinessRules)
        {
            _mapper = mapper;
            _contentRepository = contentRepository;
            _contentBusinessRules = contentBusinessRules;
        }

        public async Task<GetByIdContentResponse> Handle(GetByIdContentQuery request, CancellationToken cancellationToken)
        {
            Content? content = await _contentRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _contentBusinessRules.ContentShouldExistWhenSelected(content);

            GetByIdContentResponse response = _mapper.Map<GetByIdContentResponse>(content);
            return response;
        }
    }
}