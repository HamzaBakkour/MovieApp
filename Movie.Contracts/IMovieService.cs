using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Movie.Core;
using Movie.Core.Dtos;

namespace Movie.Contracts;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetMoviesAsync(bool trackChanges = false);
    Task<MovieDto> GetMovieAsync(int id, bool trackChanges = false);
    Task<MovieAllDetailsDto> GetMovieDetailsAsync(int id, bool trackChanges = false);
    Task<MovieDto> AddMovieAsync(MovieCreateDto dto, bool trackChanges = false);
    Task<MovieDto> DeleteMovieAsync(int id, bool trackChanges = false);
    Task<MovieDto> UpdateMovieAsync(int id, MovieUpdateDto dto, bool trackChanges = false);
    Task<MovieAllDetailsDto> PatchMovieAsync(int id, JsonPatchDocument<MoviePatchDto> patchDoc, bool trackChanges = false);
    Task<PagedResult<MovieDto>> GetMoviesAsync(MoviePagingParametersDto parameters, bool trackChanges = false);

}

