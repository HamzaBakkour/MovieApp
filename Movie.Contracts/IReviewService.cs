using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.Dtos;

namespace Movie.Contracts;

public interface IReviewService
{
    Task<IEnumerable<ReviewDetailsDto>> GetReviewsAsync(int movieId, bool trackChanges = false);
    Task<ReviewDto> AddReviewAsync(int movieId, ReviewCreateDto dto, bool trackChanges = false);
}
