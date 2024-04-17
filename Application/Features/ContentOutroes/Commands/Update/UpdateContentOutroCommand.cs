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

namespace Application.Features.ContentOutroes.Commands.Update;

public class UpdateContentOutroCommand : IRequest<UpdatedContentOutroResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentOutroesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentOutroes";

    public class UpdateContentOutroCommandHandler : IRequestHandler<UpdateContentOutroCommand, UpdatedContentOutroResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentOutroRepository _contentOutroRepository;
        private readonly ContentOutroBusinessRules _contentOutroBusinessRules;

        public UpdateContentOutroCommandHandler(IMapper mapper, IContentOutroRepository contentOutroRepository,
                                         ContentOutroBusinessRules contentOutroBusinessRules)
        {
            _mapper = mapper;
            _contentOutroRepository = contentOutroRepository;
            _contentOutroBusinessRules = contentOutroBusinessRules;
        }

        public async Task<UpdatedContentOutroResponse> Handle(UpdateContentOutroCommand request, CancellationToken cancellationToken)
        {
            ContentOutro? contentOutro = await _contentOutroRepository.GetAsync(predicate: co => co.Id == request.Id, cancellationToken: cancellationToken);
            await _contentOutroBusinessRules.ContentOutroShouldExistWhenSelected(contentOutro);
            contentOutro = _mapper.Map(request, contentOutro);

            await _contentOutroRepository.UpdateAsync(contentOutro!);

            UpdatedContentOutroResponse response = _mapper.Map<UpdatedContentOutroResponse>(contentOutro);
            return response;
        }
    }
}