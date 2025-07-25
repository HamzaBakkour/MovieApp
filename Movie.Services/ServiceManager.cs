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


    public IMovieService MovieService => _movieService.Value;
    public IReviewService ReviewService => _reviewService.Value;


    //..
    //..
    //..

    public ServiceManager(Lazy<IMovieService> movieService,
                            Lazy<IReviewService> reviewService)
    {
        this._movieService = movieService;
        this._reviewService = reviewService;

    }
}
