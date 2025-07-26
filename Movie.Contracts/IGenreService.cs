using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.Dtos;

namespace Movie.Contracts;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetGenresAsync(bool trackChanges = false);
}
