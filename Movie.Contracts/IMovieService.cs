using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.Dtos;

namespace Movie.Contracts;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetMoviesAsync(bool trackChanges = false);
    Task<MovieDto> GetMovieAsync(int id, bool trackChanges = false);
}
