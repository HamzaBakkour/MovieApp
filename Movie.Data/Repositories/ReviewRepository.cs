using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie.Core.DomainContracts;
using Movie.Data.DataConfigurations;

namespace Movie.Data.Repositories;

public class ReviewRepository : RepositoryBase<Core.DomainEntities.Review>, IReviewRepository
{

    public ReviewRepository(ApplicationDbContext context) : base(context) { }

    //public async Task<Core.DomainEntities.Review?> GetAsync(int movieId, bool trackChanges = false) =>
    //    await FindByCondition(m => m.Id == movieId, trackChanges).FirstOrDefaultAsync();

}
