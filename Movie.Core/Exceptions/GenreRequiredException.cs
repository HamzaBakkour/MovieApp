using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Exceptions;

public class GenreRequiredException : Exception
{
    public GenreRequiredException() : base($"A movie must have at least one genre") { }
}
