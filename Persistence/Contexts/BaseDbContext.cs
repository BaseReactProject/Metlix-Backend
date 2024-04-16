using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Domain.Entities;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountCreditCard> AccountCreditCards { get; set; }
    public DbSet<AccountProfile> AccountProfiles { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Quality> Qualities { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<ContentActor> ContentActors { get; set; }
    public DbSet<ContentCategory> ContentCategories { get; set; }
    public DbSet<ContentDirector> ContentDirectors { get; set; }
    public DbSet<ContentGenre> ContentGenres { get; set; }
    public DbSet<ContentNotice> ContentNotices { get; set; }
    public DbSet<ContentScenarist> ContentScenarists { get; set; }
    public DbSet<ContentTrailer> ContentTrailers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Notice> Notices { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Trailer> Trailers { get; set; }
    public DbSet<TrailerGenre> TrailerGenres { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
