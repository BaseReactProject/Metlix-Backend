using Application.Features.ContentOutroes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentOutroes.Rules;

public class ContentOutroBusinessRules : BaseBusinessRules
{
    private readonly IContentOutroRepository _contentOutroRepository;

    public ContentOutroBusinessRules(IContentOutroRepository contentOutroRepository)
    {
        _contentOutroRepository = contentOutroRepository;
    }

    public Task ContentOutroShouldExistWhenSelected(ContentOutro? contentOutro)
    {
        if (contentOutro == null)
            throw new BusinessException(ContentOutroesBusinessMessages.ContentOutroNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentOutroIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ContentOutro? contentOutro = await _contentOutroRepository.GetAsync(
            predicate: co => co.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentOutroShouldExistWhenSelected(contentOutro);
    }
}