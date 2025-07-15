using Movie.Core.DomainContracts;
using Movie.Data.DataConfigurations;

namespace Movie.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lazy<IMovieRepository> _movieRepository;
    private readonly ApplicationDbContext _context;

    public IMovieRepository MovieRepository => _movieRepository.Value;

    public UnitOfWork(
        ApplicationDbContext context,
        Lazy<IMovieRepository> movieRepository)
    {
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CompleteAsync() => await _context.SaveChangesAsync();
}