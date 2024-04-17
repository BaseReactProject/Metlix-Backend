using Application.Features.ContentIntroes.Constants;
using Application.Features.ContentIntroes.Constants;
using Application.Features.ContentIntroes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentIntroes.Constants.ContentIntroesOperationClaims;

namespace Application.Features.ContentIntroes.Commands.Delete;

public class DeleteContentIntroCommand : IRequest<DeletedContentIntroResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentIntroesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentIntroes";

    public class DeleteContentIntroCommandHandler : IRequestHandler<DeleteContentIntroCommand, DeletedContentIntroResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentIntroRepository _contentIntroRepository;
        private readonly ContentIntroBusinessRules _contentIntroBusinessRules;

        public DeleteContentIntroCommandHandler(IMapper mapper, IContentIntroRepository contentIntroRepository,
                                         ContentIntroBusinessRules contentIntroBusinessRules)
        {
            _mapper = mapper;
            _contentIntroRepository = contentIntroRepository;
            _contentIntroBusinessRules = contentIntroBusinessRules;
        }

        public async Task<DeletedContentIntroResponse> Handle(DeleteContentIntroCommand request, CancellationToken cancellationToken)
        {
            ContentIntro? contentIntro = await _contentIntroRepository.GetAsync(predicate: ci => ci.Id == request.Id, cancellationToken: cancellationToken);
            await _contentIntroBusinessRules.ContentIntroShouldExistWhenSelected(contentIntro);

            await _contentIntroRepository.DeleteAsync(contentIntro!);

            DeletedContentIntroResponse response = _mapper.Map<DeletedContentIntroResponse>(contentIntro);
            return response;
        }
    }
}