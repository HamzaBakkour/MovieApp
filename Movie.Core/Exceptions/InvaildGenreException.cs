using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Exceptions;

public class InvaildGenreException : Exception
{
    public InvaildGenreException(string gnenre) : base($"{gnenre} is not a viled genre.") { }
}
