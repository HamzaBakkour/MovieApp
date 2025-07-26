using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movie.Contracts;
using Movie.Core.DomainEntities;
using Movie.Core.Dtos;

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
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors() =>
        Ok(await serviceManager.ActorService.GetActorsAsync());



    [HttpPost]
    public async Task<ActionResult<MovieDto>> PostActor([FromBody] ActorCreateDto dto)
    {
        var response = await serviceManager.ActorService.AddActorAsync(dto);

        /// TODO: return CreatedAtAction(nameof(GetMovie), new { id = response.Id }, response);
        return Ok(response);
    }




    [HttpPost("/api/movies/{movieId}/actors/{actorId}")]
    public async Task<IActionResult> AddActorToMovie([FromRoute, Range(0, int.MaxValue)] int movieId,
                                                    [FromRoute, Range(0, int.MaxValue)] int actorId
                                                    )
    {
        var response = await serviceManager.ActorService.AddActorToMovieAsync(movieId, actorId, true);

        return Ok(response);
    }




}
