using Application.Features.ContentOutroes.Constants;
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

namespace Application.Features.ContentOutroes.Commands.Delete;

public class DeleteContentOutroCommand : IRequest<DeletedContentOutroResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentOutroesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentOutroes";

    public class DeleteContentOutroCommandHandler : IRequestHandler<DeleteContentOutroCommand, DeletedContentOutroResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentOutroRepository _contentOutroRepository;
        private readonly ContentOutroBusinessRules _contentOutroBusinessRules;

        public DeleteContentOutroCommandHandler(IMapper mapper, IContentOutroRepository contentOutroRepository,
                                         ContentOutroBusinessRules contentOutroBusinessRules)
        {
            _mapper = mapper;
            _contentOutroRepository = contentOutroRepository;
            _contentOutroBusinessRules = contentOutroBusinessRules;
        }

        public async Task<DeletedContentOutroResponse> Handle(DeleteContentOutroCommand request, CancellationToken cancellationToken)
        {
            ContentOutro? contentOutro = await _contentOutroRepository.GetAsync(predicate: co => co.Id == request.Id, cancellationToken: cancellationToken);
            await _contentOutroBusinessRules.ContentOutroShouldExistWhenSelected(contentOutro);

            await _contentOutroRepository.DeleteAsync(contentOutro!);

            DeletedContentOutroResponse response = _mapper.Map<DeletedContentOutroResponse>(contentOutro);
            return response;
        }
    }
}