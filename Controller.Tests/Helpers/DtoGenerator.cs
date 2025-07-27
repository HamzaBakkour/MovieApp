using System.IO;
using Bogus;
using Bogus.DataSets;
using Movie.Core;
using Movie.Core.Dtos;

namespace Controller.Tests.Helpers;

internal class DtoGenerator
{

    public static PagedResult<MovieDto> GeneratePagedMovies(int count = 5)
    {
        var faker = new Faker("en");

        var movies = Enumerable.Range(1, count).Select(id =>
            new MovieDto(
                id,
                faker.Lorem.Sentence(3),
                faker.Date.Past(10).Year,
                faker.Random.Int(60, 180)
            )).ToList();

        return new PagedResult<MovieDto>(
            movies,
            count: 20,
            pageNumber: 1,
            pageSize: count
        );
    }


    public static MovieAllDetailsDto GenerateMovieDetailsDto(int id)
    {
        return new MovieAllDetailsDto
        {
            Id = id,
            Title = $"Test Movie {id}",
            Year = 2000 + id,
            Duration = 60 + id,

            Detailes = new MovieDetailesDto ($"Synopsis for movie {id}", $"en{id}", 1_00_000),

            Actors = new List<ActorDto>
            {
                new ActorDto (1, "Actor One", BirthYear: 2001),
                new ActorDto (2, "Actor Two", BirthYear: 2002),

            },

            Genres = new List<GenreDto>
            {
                new GenreDto ("Action"),
                new GenreDto ("Drama"),
            },

            Reviews = new List<ReviewDto>
            {
                new ReviewDto ("Review1", "Comment1", 1),
                new ReviewDto ("Review2", "Comment2", 2)
            }
        };
    }

    public static MovieCreateDto GenerateCreateMovieDto(int genreCount = 2)
    {
        var faker = new Faker("en");

        var genreIds = Enumerable.Range(1, genreCount).ToList();

        return new MovieCreateDto
        {
            Title = faker.Lorem.Sentence(3),
            Year = faker.Date.Past(10).Year,
            Duration = faker.Random.Int(60, 180),
            Detailes = new MovieDetailesCreateDto {

                Synopsis = faker.Lorem.Sentences(2),
                Language = "en",
                Budget = faker.Random.Int(10_000, 1_000_000)
            },
            GenreIds = genreIds
        };
    }


    public static MovieCreateDto GenerateInvalidDocumentaryMovie()
    {
        var faker = new Faker("en");

        return new MovieCreateDto
        {
            Title = "Big Budget Doc",
            Year = 2022,
            Duration = 90,
            Detailes = new MovieDetailesCreateDto
            {
                Synopsis = faker.Lorem.Sentences(2),
                Language = "en",
                Budget = 2_000_000
            },
            GenreIds = new List<int> { 1 }
        };
    }

}



