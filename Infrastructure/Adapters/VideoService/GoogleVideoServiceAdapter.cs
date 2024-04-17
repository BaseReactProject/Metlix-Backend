

using Application.Services.VideoService;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Infrastructure.Adapters.VideoService.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Adapters.VideoService;

public class GoogleVideoServiceAdapter : VideoServiceBase
{
    private readonly StorageClient _storageClient;
    private readonly GoogleCloudStorageConfig? googleCloudStorageConfig;
    public GoogleVideoServiceAdapter(IConfiguration configuration)
    {
        googleCloudStorageConfig = configuration.GetSection("GoogleCloudStorageConfig").Get<GoogleCloudStorageConfig>()??
            throw new ArgumentException("Can't Found GoogleCloudStorageConfing in the appssettings.json");
        GoogleCredential credential = GoogleCredential.FromFile(Path.Combine("metflix-420612-83e6928014a7.json"));
        _storageClient = StorageClient.Create(credential);
    }
    public override async Task<string> UploadAsync(IFormFile file)
    {
        await FileMustBeInVideoFormat(file);

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

       Google.Apis.Storage.v1.Data.Object updatedVideo =  await _storageClient.UploadObjectAsync
            (
           googleCloudStorageConfig.BucketName,
           Guid.NewGuid()+file.Name, 
            null,
            memoryStream
           );

        return updatedVideo.MediaLink;
    }
}

