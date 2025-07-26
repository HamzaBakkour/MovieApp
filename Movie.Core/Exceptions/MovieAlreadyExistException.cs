using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Exceptions;

public class MovieAlreadyExistException : Exception
{
    public MovieAlreadyExistException(string tittle) : base($"A movie with the title ({tittle}) already exist.") { }
}
