using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movie.Contracts;
using Movie.Core.Dtos;

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
    public async Task<ActionResult<ReviewDetailsDto>> PostReview([FromRoute, Range(0, int.MaxValue)] int movieId, [FromBody] ReviewCreateDto dto)
    {
        var response = await serviceManager.ReviewService.AddReviewAsync(movieId, dto);
        /// TODO: return CreatedAtAction(nameof(GetMovieReviews), new { movieId = movie.Id }, response);
        return Ok(response);
    }

    [HttpGet("/api/movies/{movieId}/reviews")]
    public async Task<ActionResult<ReviewDetailsDto>> GetMovieReviews([FromRoute, Range(0, int.MaxValue)] int movieId)
    { 
        var response = await serviceManager.ReviewService.GetReviewsAsync(movieId);
        return Ok(response);
    }

}
