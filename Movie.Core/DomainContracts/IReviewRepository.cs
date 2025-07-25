using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.DomainContracts;

public interface IReviewRepository : IRepositoryBase<DomainEntities.Review>
{
    //Task<DomainEntities.Review?> GetAsync(int movieId, bool trackChanges = false);
}
