namespace Movie.Core.Exceptions;

public class MovieNotFoundException : NotFoundException
{
    public MovieNotFoundException(int id) : base($"The movie with id: {id} was not found") { }
}
