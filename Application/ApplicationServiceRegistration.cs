using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.ElasticSearch;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Services.Accounts;
using Application.Services.AccountCreditCards;
using Application.Services.AccountProfiles;
using Application.Services.Images;
using Application.Services.Plans;
using Application.Services.Profiles;
using Application.Services.Qualities;
using Application.Services.JWT;
using Application.Services.Contents;
using Application.Services.ContentActors;
using Application.Services.ContentCategories;
using Application.Services.ContentDirectors;
using Application.Services.ContentGenres;
using Application.Services.ContentNotices;
using Application.Services.ContentScenarists;
using Application.Services.ContentTrailers;
using Application.Services.Genres;
using Application.Services.Movies;
using Application.Services.Notices;
using Application.Services.People;
using Application.Services.Trailers;
using Application.Services.TrailerGenres;
using Application.Services.ContentIntroes;
using Application.Services.ContentOutroes;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMailService, MailKitMailService>();
        services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<IElasticSearch, ElasticSearchManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IJwtHelper, JwtHelper>();

        services.AddScoped<IAccountsService, AccountsManager>();
        services.AddScoped<IAccountCreditCardsService, AccountCreditCardsManager>();
        services.AddScoped<IAccountProfilesService, AccountProfilesManager>();
        services.AddScoped<IImagesService, ImagesManager>();
        services.AddScoped<IPlansService, PlansManager>();
        services.AddScoped<IProfilesService, ProfilesManager>();
        services.AddScoped<IQualitiesService, QualitiesManager>();
        services.AddScoped<IContentsService, ContentsManager>();
        services.AddScoped<IContentActorsService, ContentActorsManager>();
        services.AddScoped<IContentCategoriesService, ContentCategoriesManager>();
        services.AddScoped<IContentDirectorsService, ContentDirectorsManager>();
        services.AddScoped<IContentGenresService, ContentGenresManager>();
        services.AddScoped<IContentNoticesService, ContentNoticesManager>();
        services.AddScoped<IContentScenaristsService, ContentScenaristsManager>();
        services.AddScoped<IContentTrailersService, ContentTrailersManager>();
        services.AddScoped<IGenresService, GenresManager>();
        services.AddScoped<IMoviesService, MoviesManager>();
        services.AddScoped<INoticesService, NoticesManager>();
        services.AddScoped<IPeopleService, PeopleManager>();
        services.AddScoped<ITrailersService, TrailersManager>();
        services.AddScoped<ITrailerGenresService, TrailerGenresManager>();
        services.AddScoped<IContentIntroesService, ContentIntroesManager>();
        services.AddScoped<IContentOutroesService, ContentOutroesManager>();
        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
