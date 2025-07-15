using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Movie.Contracts;
using Movie.Core.DomainContracts;
using Movie.Core.Dtos;

namespace Movie.Services;

public class MovieService : IMovieService
{

    private IUnitOfWork uow;
    private readonly IMapper mapper;


    public MovieService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }


    public async Task<MovieDto> GetMovieAsync(int id, bool trackChanges = false)
    {
        var movie = await uow.MovieRepository.GetAsync(id, trackChanges);

        if (movie is null) return null!;

        return mapper.Map<MovieDto>(movie);
    }

    public async Task<IEnumerable<MovieDto>> GetMoviesAsync(bool trackChanges = false)
    {
        return mapper.Map<IEnumerable<MovieDto>>(await uow.MovieRepository.GetAllAsync(trackChanges));
    }
}
