using Movie.Core.Dtos;

namespace Movie.Core.DomainContracts;

public interface IMovieRepository : IRepositoryBase<DomainEntities.Movie>
{
    Task<List<DomainEntities.Movie>> GetAllAsync(bool trackChanges = false);
    Task<DomainEntities.Movie?> GetAsync(int id, bool trackChanges = false);
    Task<Core.DomainEntities.Movie?> GetReviewsAsync(int id, bool trackChanges = false);
    Task<bool> AnyAsync(int id, bool trackChanges = false);
    Task<DomainEntities.Movie?> GetDetailsAsync(int id, bool trackChanges = false);
}
