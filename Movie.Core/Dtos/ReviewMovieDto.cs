namespace Movie.Core.Dtos;


public record ReviewMovieDto(MovieDto Movie,
                                List<ReviewDto> Reviews);
