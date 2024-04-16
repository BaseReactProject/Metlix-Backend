

using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace Application.Services.VideoService;

public abstract class VideoServiceBase
{
    public abstract Task<string> UploadAsync(IFormFile formFile);

    protected async Task FileMustBeInVideoFormat(IFormFile formFile)
    {
        List<string> extensions = new() { ".mp4", ".awi" };

        string extension = Path.GetExtension(formFile.FileName).ToLower();
        if (!extensions.Contains(extension))
            throw new BusinessException("Unsupported format");
        await Task.CompletedTask;
    }
}
