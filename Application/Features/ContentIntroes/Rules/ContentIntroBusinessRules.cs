using Application.Features.ContentIntroes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentIntroes.Rules;

public class ContentIntroBusinessRules : BaseBusinessRules
{
    private readonly IContentIntroRepository _contentIntroRepository;

    public ContentIntroBusinessRules(IContentIntroRepository contentIntroRepository)
    {
        _contentIntroRepository = contentIntroRepository;
    }

    public Task ContentIntroShouldExistWhenSelected(ContentIntro? contentIntro)
    {
        if (contentIntro == null)
            throw new BusinessException(ContentIntroesBusinessMessages.ContentIntroNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentIntroIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ContentIntro? contentIntro = await _contentIntroRepository.GetAsync(
            predicate: ci => ci.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentIntroShouldExistWhenSelected(contentIntro);
    }
}