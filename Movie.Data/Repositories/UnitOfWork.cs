using Movie.Core.DomainContracts;
using Movie.Data.DataConfigurations;

namespace Movie.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lazy<IMovieRepository> _movieRepository;
    private readonly Lazy<IReviewRepository> _reviewRepository;
    private readonly Lazy<IActorRepository> _actorRepository;
    private readonly Lazy<IGenreRepository> _genreRepository;




    private readonly ApplicationDbContext _context;

    public IMovieRepository MovieRepository => _movieRepository.Value;
    public IReviewRepository ReviewRepository => _reviewRepository.Value;
    public IActorRepository ActorRepository => _actorRepository.Value;
    public IGenreRepository GenreRepository => _genreRepository.Value;



    public UnitOfWork(
        ApplicationDbContext context,
        Lazy<IMovieRepository> movieRepository,
        Lazy<IReviewRepository> reviewRepository,
        Lazy<IActorRepository> actorRepository,
        Lazy<IGenreRepository> genreRepository

        )
    {
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        _actorRepository = actorRepository ?? throw new ArgumentNullException(nameof(actorRepository));
        _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));



        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CompleteAsync() => await _context.SaveChangesAsync();
}