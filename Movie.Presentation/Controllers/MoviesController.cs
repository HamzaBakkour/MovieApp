using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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



    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies() =>
    //                                      Ok(await serviceManager.MovieService.GetMoviesAsync());

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery] MoviePagingParametersDto parameters)
    {
        var pagedResult = await serviceManager.MovieService.GetMoviesAsync(parameters);

        var metadata = new
        {
            pagedResult.TotalCount,
            pagedResult.PageSize,
            pagedResult.CurrentPage,
            pagedResult.TotalPages
        };

        Response.Headers.Append("X-Pagination", new StringValues(JsonSerializer.Serialize(metadata)));
        Response.Headers.Append("Access-Control-Expose-Headers", new StringValues("X-Pagination"));

        return Ok(pagedResult);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie([FromRoute, Range(0, int.MaxValue)] int id) =>
        Ok(await serviceManager.MovieService.GetMovieAsync(id));



    [HttpGet("{id}/details")]
    public async Task<ActionResult<MovieAllDetailsDto>> GetMovieDetails([FromRoute, Range(0, int.MaxValue)] int id) =>
                                                    Ok(await serviceManager.MovieService.GetMovieDetailsAsync(id));


    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie([FromRoute, Range(0, int.MaxValue)] int id, [FromBody] MovieUpdateDto dto) =>
                                                    Ok(await serviceManager.MovieService.UpdateMovieAsync(id, dto));


    [HttpPost]
    public async Task<ActionResult<MovieDto>> PostMovie([FromBody] MovieCreateDto dto)
    {
        var response = await serviceManager.MovieService.AddMovieAsync(dto);
        return CreatedAtAction(nameof(GetMovie), new { id = response.Id }, response);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie([FromRoute, Range(0, int.MaxValue)] int id)
    {
        //var response = await serviceManager.MovieService.DeleteMovieAsync(id);
        await serviceManager.MovieService.DeleteMovieAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchMovie([FromRoute, Range(0, int.MaxValue)] int id, [FromBody] JsonPatchDocument<MoviePatchDto> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest();

        var updatedMovie = await serviceManager.MovieService.PatchMovieAsync(id, patchDoc);

        if (updatedMovie is null)
            return NotFound();

        return Ok(updatedMovie);
    }


    //private bool MovieExists(int id)
    //{
    //    return _context.Movies.Any(e => e.Id == id);
    //}
}
