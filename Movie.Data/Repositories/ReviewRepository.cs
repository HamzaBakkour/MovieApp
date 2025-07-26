using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie.Core;
using Movie.Core.DomainContracts;
using Movie.Core.DomainEntities;
using Movie.Data.DataConfigurations;

namespace Movie.Data.Repositories;

public class ReviewRepository : RepositoryBase<Core.DomainEntities.Review>, IReviewRepository
{

    public ReviewRepository(ApplicationDbContext context) : base(context) { }

    public async Task<PagedResult<Review>> GetPagedReviewsAsync(int movieId, int pageNumber, int pageSize, bool trackChanges)
    {
        var query = FindByCondition(r => r.MovieId == movieId, trackChanges)
                    .OrderBy(r => r.Id);

        return await PagedResult<Review>.ToPagedListAsync(query, pageNumber, pageSize);
    }

}
