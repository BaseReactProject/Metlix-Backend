using Application.Features.ContentTrailers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentTrailers.Rules;

public class ContentTrailerBusinessRules : BaseBusinessRules
{
    private readonly IContentTrailerRepository _contentTrailerRepository;

    public ContentTrailerBusinessRules(IContentTrailerRepository contentTrailerRepository)
    {
        _contentTrailerRepository = contentTrailerRepository;
    }

    public Task ContentTrailerShouldExistWhenSelected(ContentTrailer? contentTrailer)
    {
        if (contentTrailer == null)
            throw new BusinessException(ContentTrailersBusinessMessages.ContentTrailerNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentTrailerIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ContentTrailer? contentTrailer = await _contentTrailerRepository.GetAsync(
            predicate: ct => ct.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentTrailerShouldExistWhenSelected(contentTrailer);
    }
}