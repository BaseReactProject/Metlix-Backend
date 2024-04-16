

using Application.Services.VideoService;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Adapters.VideoService;

public class FileVideoServiceAdapter : VideoServiceBase
{
    public override async Task<string> UploadAsync(IFormFile formFile)
    {
        await FileMustBeInVideoFormat(formFile);
        var saveFolder = @"D:\Videos";
        var fileName = formFile.FileName;
        var filePath = Path.Combine(saveFolder, fileName);

        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return filePath;
        }
        catch (Exception ex)
        {
            throw new Exception("Dosya yükleme hatası: " + ex.Message);
        }

    }
}
