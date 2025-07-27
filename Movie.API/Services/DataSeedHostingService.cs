using System.Globalization;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Movie.Core.DomainEntities;
using Movie.Data.DataConfigurations;

namespace Movie.API.Services;

public class DataSeedHostingService : IHostedService
{


    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DataSeedHostingService> logger;

    private readonly int numberOfMovies = 2;
    private readonly int numberOfActors = 2;
    private readonly int numberOfGenres = 2;
    private readonly int maxNumberOfReviews = 2;

    public DataSeedHostingService(IServiceProvider serviceProvider, ILogger<DataSeedHostingService> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        if (!env.IsDevelopment()) return;

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (await context.Movies.AnyAsync(cancellationToken)) return;

        try
        {
            var genres = GenerateGenres(numberOfGenres);
            var actors = GenerateActors(numberOfActors);
            var movies = GenerateMovies(numberOfMovies, actors, genres);

            await context.Genres.AddRangeAsync(genres, cancellationToken);
            await context.Actors.AddRangeAsync(actors, cancellationToken);
            await context.Movies.AddRangeAsync(movies, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Movie data seeding completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Movie data seeding failed.");
            throw;
        }

    }


    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;


    private List<Genre> GenerateGenres(int count)
    {
        var faker = new Faker("en");
        var genres = new List<Genre>();

        genres.Add(new Genre { Name = "Documentary" });

        while (genres.Count < count)
        {
            var genreName = faker.Music.Genre();

            /// Avoid duplicates
            if (!genres.Any(g => string.Equals(g.Name, genreName, StringComparison.OrdinalIgnoreCase)))
            {
                genres.Add(new Genre { Name = genreName });
            }
        }

        return genres;
    }

    private List<Actor> GenerateActors(int count)
    {
        var faker = new Faker("en");
        var currentYear = DateTime.UtcNow.Year;

        return Enumerable.Range(0, count).Select(_ =>
        {
            var birthYear = faker.Date.Between(
                new DateTime(currentYear - 90, 1, 1),
                new DateTime(currentYear - 12, 1, 1)
            ).Year;

            return new Actor
            {
                Name = faker.Name.FullName(),
                BirthYear = birthYear
            };
        }).ToList();
    }

    private List<Review> GenerateReviews(int count)
    {
        var faker = new Faker("en");
        return Enumerable.Range(1, count).Select(_ => new Review
        {
            ReviewerName = faker.Name.FullName(),
            Comment = faker.Lorem.Sentence(),
            Rating = faker.Random.Int(1, 5)
        }).ToList();
    }


    private List<Core.DomainEntities.Movie> GenerateMovies(int count, List<Actor> actors, List<Genre> genres)
    {
        var faker = new Faker("en");
        var currentYear = DateTime.UtcNow.Year;

        return Enumerable.Range(0, count).Select(_ =>
        {
            return new Core.DomainEntities.Movie
            {
                Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Commerce.ProductName()),
                Year = faker.Date.Between(new DateTime(currentYear - 40, 1, 1), DateTime.UtcNow).Year,
                Duration = faker.Random.Int(60, 150),
                Genres = faker.PickRandom(genres, faker.Random.Int(1, genres.Count)).ToList(),
                Actors = faker.PickRandom(actors, faker.Random.Int(1, actors.Count)).ToList(),
                Detailes = new MovieDetailes
                {
                    Synopsis = faker.Lorem.Paragraph(),
                    Language = faker.PickRandom(new[] { "sv", "en", "fr", "de", "ar" }),
                    Budget = faker.Random.Int(1_000_000, 100_000_000)
                },
                Reviews = GenerateReviews(faker.Random.Int(0, maxNumberOfReviews))
            };
        }).ToList();
    }


}
