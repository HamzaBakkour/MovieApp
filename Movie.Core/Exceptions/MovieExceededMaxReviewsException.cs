using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Exceptions;

public class MovieExceededMaxReviewsException : Exception
{
    public MovieExceededMaxReviewsException(int id, int num) : base($"The movie with id: {id} has exceeded max number of reviews ({num})") { }
}
