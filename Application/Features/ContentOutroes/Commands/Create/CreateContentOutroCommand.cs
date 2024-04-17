using Application.Features.ContentOutroes.Constants;
using Application.Features.ContentOutroes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentOutroes.Constants.ContentOutroesOperationClaims;

namespace Application.Features.ContentOutroes.Commands.Create;

public class CreateContentOutroCommand : IRequest<CreatedContentOutroResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int ContentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string[] Roles => new[] { Admin, Write, ContentOutroesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentOutroes";

    public class CreateContentOutroCommandHandler : IRequestHandler<CreateContentOutroCommand, CreatedContentOutroResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentOutroRepository _contentOutroRepository;
        private readonly ContentOutroBusinessRules _contentOutroBusinessRules;

        public CreateContentOutroCommandHandler(IMapper mapper, IContentOutroRepository contentOutroRepository,
                                         ContentOutroBusinessRules contentOutroBusinessRules)
        {
            _mapper = mapper;
            _contentOutroRepository = contentOutroRepository;
            _contentOutroBusinessRules = contentOutroBusinessRules;
        }

        public async Task<CreatedContentOutroResponse> Handle(CreateContentOutroCommand request, CancellationToken cancellationToken)
        {
            ContentOutro contentOutro = _mapper.Map<ContentOutro>(request);

            await _contentOutroRepository.AddAsync(contentOutro);

            CreatedContentOutroResponse response = _mapper.Map<CreatedContentOutroResponse>(contentOutro);
            return response;
        }
    }
}