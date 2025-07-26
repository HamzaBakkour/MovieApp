using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.DomainEntities;

namespace Movie.Core.DomainContracts;

public interface IReviewRepository : IRepositoryBase<DomainEntities.Review>
{
    Task<PagedResult<Review>> GetPagedReviewsAsync(int movieId, int pageNumber, int pageSize, bool trackChanges);
}
