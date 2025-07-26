using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core;
using Movie.Core.Dtos;

namespace Movie.Contracts;

public interface IReviewService
{
    Task<PagedResult<ReviewDetailsDto>> GetReviewsAsync(int movieId, ReviewPagingParametersDto parameters, bool trackChanges = false);
    Task<ReviewDto> AddReviewAsync(int movieId, ReviewCreateDto dto, bool trackChanges = false);
}
