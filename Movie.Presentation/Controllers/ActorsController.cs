using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Contracts;
using Movie.Core.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Movie.Presentation.Controllers;


[Route("api/actors")]
[ApiController]
[Produces("application/json")]
public class ActorsController : ControllerBase
{

    private readonly IServiceManager serviceManager;

    public ActorsController(IServiceManager serviceManager)
    {
        this.serviceManager = serviceManager;
    }


    [HttpGet]
    [SwaggerOperation(Summary = "Get all actors", Description = "Gets all actors.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDto>))]
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors() =>
        Ok(await serviceManager.ActorService.GetActorsAsync());



    [HttpPost]
    [SwaggerOperation(Summary = "Create actor", Description = "Creates a new actor.")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ActorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ActorDto>> PostActor([FromBody] ActorCreateDto dto)
    {
        var response = await serviceManager.ActorService.AddActorAsync(dto);

        return Created(string.Empty, response);
    }




    [HttpPost("/api/movies/{movieId}/actors/{actorId}")]
    [SwaggerOperation(Summary = "Add actor to a movie", Description = "Add an existing actor to an existing movie.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddActorToMovie([FromRoute, Range(0, int.MaxValue)] int movieId,
                                                    [FromRoute, Range(0, int.MaxValue)] int actorId
                                                    )
    {
        var response = await serviceManager.ActorService.AddActorToMovieAsync(movieId, actorId, true);

        return Ok(response);
    }




}
