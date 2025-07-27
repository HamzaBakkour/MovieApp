using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Movie.Contracts;
using Movie.Core.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Movie.Presentation.Controllers;

[Route("api/reviews")]
[ApiController]
[Produces("application/json")]
public class ReviewsController : ControllerBase
{

    private readonly IServiceManager serviceManager;

    public ReviewsController(IServiceManager serviceManager)
    {
        this.serviceManager = serviceManager;
    }


    [HttpPost("/api/movies/{movieId}/reviews")]
    [SwaggerOperation(Summary = "Add review to a movie", Description = "Add review to an existing movie.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewDetailsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReviewDetailsDto>> PostReview([FromRoute, Range(0, int.MaxValue)] int movieId, [FromBody] ReviewCreateDto dto)
    {
        var response = await serviceManager.ReviewService.AddReviewAsync(movieId, dto);
        return Created(string.Empty, response);
    }

    [HttpGet("/api/movies/{movieId}/reviews")]
    [SwaggerOperation(Summary = "Get all reviews", Description = "Gets all reviews.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDetailsDto>))]
    public async Task<ActionResult<IEnumerable<ReviewDetailsDto>>> GetReviews(
        [FromRoute] int movieId,
        [FromQuery] ReviewPagingParametersDto parameters)
    {
        var pagedResult = await serviceManager.ReviewService.GetReviewsAsync(movieId, parameters);

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

}
