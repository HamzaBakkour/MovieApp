using Microsoft.EntityFrameworkCore;
using Movie.Core.DomainContracts;
using Movie.Core.Dtos;
using Movie.Data.DataConfigurations;


namespace Movie.Data.Repositories;

public class MovieRepository : RepositoryBase<Core.DomainEntities.Movie>, IMovieRepository
{
    public MovieRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<Core.DomainEntities.Movie>> GetAllAsync(bool trackChanges = false) =>
                                                            await FindAll(trackChanges).ToListAsync();

    public async Task<Core.DomainEntities.Movie?> GetAsync(int id, bool trackChanges = false) =>
                                                            await FindByCondition(m => m.Id == id, trackChanges).FirstOrDefaultAsync();

    public async Task<bool> AnyAsync(int id, bool trackChanges = false) =>
                                                            await FindByCondition(m => m.Id == id, trackChanges).AnyAsync();

    public async Task<Core.DomainEntities.Movie?> GetDetailsAsync(int id, bool trackChanges = false) =>
                                                            await FindByCondition(m => m.Id == id, trackChanges)
                                                                    .Include(m => m.Actors)
                                                                    .Include(m => m.Genres)
                                                                    .Include(m => m.Reviews)
                                                                    .Include(m => m.Detailes)
                                                                    .FirstOrDefaultAsync();
}
