using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Contracts;

namespace Movie.Services;

public class ServiceManager : IServiceManager
{

    private Lazy<IMovieService> movieService;

    public IMovieService MovieService => movieService.Value;

    //..
    //..
    //..

    public ServiceManager(Lazy<IMovieService> movieService)
    {
        this.movieService = movieService;
    }
}
