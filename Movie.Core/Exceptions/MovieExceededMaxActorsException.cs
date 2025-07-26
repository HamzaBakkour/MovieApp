using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Exceptions;

public class MovieExceededMaxActorsException : Exception
{
    public MovieExceededMaxActorsException(int movieId, int maxActors, string genre = "")
        : base(
            $"The movie with id: {movieId} has exceeded the maximum number of actors ({maxActors})" +
            (!string.IsNullOrWhiteSpace(genre) ? $" for genre '{genre}'." : ".")
        )
    {
    }
}
