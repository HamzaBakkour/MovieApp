using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Movie.Contracts;
using Movie.Core.Dtos;

namespace MovieApi.Controllers;

[Route("api/movies")]
[ApiController]
[Produces("application/json")]
public class MoviesController : ControllerBase
{
    //private readonly MovieContext _context;
    //private readonly IMapper mapper;

    private readonly IServiceManager serviceManager;

    public MoviesController(IServiceManager serviceManager)
    {
        this.serviceManager = serviceManager;
    }



    [HttpGet]
    //[SwaggerOperation(Summary = "Get all movies", Description = "Gets all movies.")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieDto>))]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    {
        return Ok(await serviceManager.MovieService.GetMoviesAsync());


    }

    //[HttpGet("{id}")]
    //[SwaggerOperation(Summary = "Get movie by ID", Description = "Returns full details of a movie.")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieAllDetailsDto))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<MovieAllDetailsDto>> GetMovie([FromRoute, Range(0, int.MaxValue)] int id,
    //                                                                [FromQuery] MovieQueryOptionsDto options)
    //{
    //    IQueryable<Movie> query = _context.Movies.Where(m => m.Id == id);

    //    if (options.withActors)
    //        query = query.Include(m => m.Actors);

    //    if (options.withGenres)
    //        query = query.Include(m => m.Genres);

    //    if (options.withReviews)
    //        query = query.Include(m => m.Reviews);

    //    if (options.withDetails)
    //        query = query.Include(m => m.Detailes);

    //    var movie = await query.FirstOrDefaultAsync();

    //    if (movie == null)
    //        return NotFound();



    //    var response = mapper.Map<MovieAllDetailsDto>(movie);

    //    return Ok(response);
    //}



    //[HttpGet("{id}/details")]
    //[SwaggerOperation(Summary = "Get movie detiales by ID", Description = "Returns full details of a movie.")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieAllDetailsDto))]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<MovieAllDetailsDto>> GetMovieDetails([FromRoute, Range(0, int.MaxValue)] int id)
    //{


    //    var movie = await _context.Movies
    //    .Include(m => m.Actors)
    //    .Include(m => m.Genres)
    //    .Include(m => m.Reviews)
    //    .Include(m => m.Detailes)
    //    .FirstOrDefaultAsync(m => m.Id == id);

    //    var response = mapper.Map<MovieAllDetailsDto>(movie);

    //    if (response == null)
    //        return NotFound();

    //    return Ok(response);

    //}



    //[HttpPut("{id}")]
    //[SwaggerOperation(Summary = "Update movie", Description = "Updates an existing movie by ID.")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieUpdateDto))]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    //public async Task<IActionResult> PutMovie([FromRoute, Range(0, int.MaxValue)] int id, [FromBody] MovieUpdateDto dto)
    //{
    //    var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

    //    if (movie is null)
    //        return NotFound();


    //    mapper.Map(dto, movie);

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!MovieExists(id))
    //            return NotFound();
    //        else
    //            throw;
    //    }

    //    var response = mapper.Map<MovieDto>(movie);

    //    return Ok(response);
    //}

    //[HttpPost]
    //[SwaggerOperation(Summary = "Create movie", Description = "Creates a new movie.")]
    //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<MovieDto>> PostMovie([FromBody] MovieCreateDto dto)
    //{

    //    var movie = mapper.Map<Movie>(dto);
    //    var movieDetails = mapper.Map<MovieDetailes>(dto.Detailes);

    //    movie.Detailes = movieDetails;

    //    _context.Movies.Add(movie);
    //    await _context.SaveChangesAsync();

    //    //var response = new MovieDto(movie.Id, movie.Title, movie.Year, movie.Duration);
    //    var response = mapper.Map<MovieDto>(movie);
    //    return CreatedAtAction(nameof(GetMovie), new { id = response.Id }, response);
    //}

    //[HttpDelete("{id}")]
    //[SwaggerOperation(Summary = "Delete movie", Description = "Deletes a movie by ID.")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteMovie([FromRoute, Range(0, int.MaxValue)] int id)
    //{
    //    var movie = await _context.Movies.FindAsync(id);
    //    if (movie == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Movies.Remove(movie);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool MovieExists(int id)
    //{
    //    return _context.Movies.Any(e => e.Id == id);
    //}
}
