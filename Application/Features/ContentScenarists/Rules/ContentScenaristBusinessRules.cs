using Application.Features.ContentScenarists.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentScenarists.Rules;

public class ContentScenaristBusinessRules : BaseBusinessRules
{
    private readonly IContentScenaristRepository _contentScenaristRepository;

    public ContentScenaristBusinessRules(IContentScenaristRepository contentScenaristRepository)
    {
        _contentScenaristRepository = contentScenaristRepository;
    }

    public Task ContentScenaristShouldExistWhenSelected(ContentScenarist? contentScenarist)
    {
        if (contentScenarist == null)
            throw new BusinessException(ContentScenaristsBusinessMessages.ContentScenaristNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentScenaristIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ContentScenarist? contentScenarist = await _contentScenaristRepository.GetAsync(
            predicate: cs => cs.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentScenaristShouldExistWhenSelected(contentScenarist);
    }
}