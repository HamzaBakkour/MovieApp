using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.DomainEntities;

namespace Movie.Core.DomainContracts;

public interface IMovieRepository : IRepositoryBase<Movie.Core.DomainEntities.Movie>
{
    Task<List<Movie.Core.DomainEntities.Movie>> GetAllAsync(bool trackChanges = false);
    Task<Movie.Core.DomainEntities.Movie?> GetAsync(int id, bool trackChanges = false);
    Task<bool> AnyAsync(int id, bool trackChanges = false);

}
