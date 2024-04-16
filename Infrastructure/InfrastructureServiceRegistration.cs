using Application.Services.ImageService;
using Application.Services.MailService;
using Application.Services.VideoService;
using Infrastructure.Adapters.ImageService;
using Infrastructure.Adapters.MailService;
using Infrastructure.Adapters.VideoService;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();
        services.AddScoped<MailServiceBase,MailKitMailService>();
        services.AddScoped<VideoServiceBase, FileVideoServiceAdapter>();
        return services;
    }
}
