

namespace Infrastructure.Adapters.VideoService.Entity;
public class GoogleCloudStorageConfig
{
    public GoogleCloudStorageConfig()
    {
        Key = string.Empty;
        BucketName = string.Empty;
    }

    public GoogleCloudStorageConfig(string key, string bucketName)
    {
        Key = key;
        BucketName = bucketName;
    }

    public string Key { get; set; }
    public string BucketName { get; set; }
}
