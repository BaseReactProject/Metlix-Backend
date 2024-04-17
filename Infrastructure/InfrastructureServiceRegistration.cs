using Application.Services.ImageService;
using Application.Services.MailService;
using Application.Services.VideoService;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Infrastructure.Adapters.ImageService;
using Infrastructure.Adapters.MailService;
using Infrastructure.Adapters.VideoService;
using Infrastructure.Adapters.VideoService.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();
        services.AddScoped<MailServiceBase,MailKitMailService>();

        services.AddSingleton(configuration.GetSection("GoogleCloudStorageConfig").Get<GoogleCloudStorageConfig>());
        services.AddScoped<VideoServiceBase, GoogleVideoServiceAdapter>();
        return services;
    }
}
