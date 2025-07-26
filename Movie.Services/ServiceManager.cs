using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Contracts;

namespace Movie.Services;

public class ServiceManager : IServiceManager
{

    private Lazy<IMovieService> _movieService;
    private Lazy<IReviewService> _reviewService;
    private Lazy<IActorService> _actorService;



    public IMovieService MovieService => _movieService.Value;
    public IReviewService ReviewService => _reviewService.Value;
    public IActorService ActorService => _actorService.Value;



    //..
    //..
    //..

    public ServiceManager(Lazy<IMovieService> movieService,
                            Lazy<IReviewService> reviewService,
                            Lazy<IActorService> actorService
                            )
    {
        this._movieService = movieService;
        this._reviewService = reviewService;
        this._actorService = actorService;
    }
}
