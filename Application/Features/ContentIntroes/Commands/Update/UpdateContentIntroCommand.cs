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

namespace Application.Features.ContentIntroes.Commands.Update;

public class UpdateContentIntroCommand : IRequest<UpdatedContentIntroResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string[] Roles => new[] { Admin, Write, ContentIntroesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentIntroes";

    public class UpdateContentIntroCommandHandler : IRequestHandler<UpdateContentIntroCommand, UpdatedContentIntroResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentIntroRepository _contentIntroRepository;
        private readonly ContentIntroBusinessRules _contentIntroBusinessRules;

        public UpdateContentIntroCommandHandler(IMapper mapper, IContentIntroRepository contentIntroRepository,
                                         ContentIntroBusinessRules contentIntroBusinessRules)
        {
            _mapper = mapper;
            _contentIntroRepository = contentIntroRepository;
            _contentIntroBusinessRules = contentIntroBusinessRules;
        }

        public async Task<UpdatedContentIntroResponse> Handle(UpdateContentIntroCommand request, CancellationToken cancellationToken)
        {
            ContentIntro? contentIntro = await _contentIntroRepository.GetAsync(predicate: ci => ci.Id == request.Id, cancellationToken: cancellationToken);
            await _contentIntroBusinessRules.ContentIntroShouldExistWhenSelected(contentIntro);
            contentIntro = _mapper.Map(request, contentIntro);

            await _contentIntroRepository.UpdateAsync(contentIntro!);

            UpdatedContentIntroResponse response = _mapper.Map<UpdatedContentIntroResponse>(contentIntro);
            return response;
        }
    }
}