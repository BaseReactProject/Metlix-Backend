using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("basedb"));

        services.AddDbContext<BaseDbContext>(
                        options => options
                        .UseSqlServer(configuration
                        .GetConnectionString
                        ("BaseDb")
                    ));
        services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountCreditCardRepository, AccountCreditCardRepository>();
        services.AddScoped<IAccountProfileRepository, AccountProfileRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IQualityRepository, QualityRepository>();
        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IContentActorRepository, ContentActorRepository>();
        services.AddScoped<IContentCategoryRepository, ContentCategoryRepository>();
        services.AddScoped<IContentDirectorRepository, ContentDirectorRepository>();
        services.AddScoped<IContentGenreRepository, ContentGenreRepository>();
        services.AddScoped<IContentNoticeRepository, ContentNoticeRepository>();
        services.AddScoped<IContentScenaristRepository, ContentScenaristRepository>();
        services.AddScoped<IContentTrailerRepository, ContentTrailerRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<INoticeRepository, NoticeRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ITrailerRepository, TrailerRepository>();
        services.AddScoped<ITrailerGenreRepository, TrailerGenreRepository>();
        return services;
    }
}
