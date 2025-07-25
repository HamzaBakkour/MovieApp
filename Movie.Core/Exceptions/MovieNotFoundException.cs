namespace Movie.Core.Exceptions;

public class MovieNotFoundException : NotFoundException
{
    public MovieNotFoundException(int id) : base($"The movie with id: {id} was not found") { }
}


public class MovieExceededMaxReviewsException : Exception
{
    public MovieExceededMaxReviewsException(int id, int num) : base($"The movie with id: {id} has exceeded max number of reviews ({num})") { }
}
