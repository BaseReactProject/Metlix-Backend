using Application.Features.ContentDirectors.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentDirectors.Rules;

public class ContentDirectorBusinessRules : BaseBusinessRules
{
    private readonly IContentDirectorRepository _contentDirectorRepository;

    public ContentDirectorBusinessRules(IContentDirectorRepository contentDirectorRepository)
    {
        _contentDirectorRepository = contentDirectorRepository;
    }

    public Task ContentDirectorShouldExistWhenSelected(ContentDirector? contentDirector)
    {
        if (contentDirector == null)
            throw new BusinessException(ContentDirectorsBusinessMessages.ContentDirectorNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentDirectorIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ContentDirector? contentDirector = await _contentDirectorRepository.GetAsync(
            predicate: cd => cd.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentDirectorShouldExistWhenSelected(contentDirector);
    }
}