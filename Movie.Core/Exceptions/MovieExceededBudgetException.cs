using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Exceptions;

public class MovieExceededBudgetException : Exception
{

    public MovieExceededBudgetException(int budget, string genre = "")
    : base(
        $"The movie has exceeded the maximum budget ({budget})" +
        (!string.IsNullOrWhiteSpace(genre) ? $" for genre '{genre}'." : ".")
    )
    {
    }

}


