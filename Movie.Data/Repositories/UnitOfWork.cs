using Movie.Core.DomainContracts;
using Movie.Data.DataConfigurations;

namespace Movie.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lazy<IMovieRepository> _movieRepository;
    private readonly Lazy<IReviewRepository> _reviewRepository;

    private readonly ApplicationDbContext _context;

    public IMovieRepository MovieRepository => _movieRepository.Value;
    public IReviewRepository ReviewRepository => _reviewRepository.Value;


    public UnitOfWork(
        ApplicationDbContext context,
        Lazy<IMovieRepository> movieRepository,
        Lazy<IReviewRepository> reviewRepository)
    {
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));

        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CompleteAsync() => await _context.SaveChangesAsync();
}