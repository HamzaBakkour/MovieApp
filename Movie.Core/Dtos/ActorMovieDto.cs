namespace Movie.Core.Dtos;



public record ActorMovieDto(int MovieId,
                                string Title,
                                int Year,
                                List<ActorDto> Actors);