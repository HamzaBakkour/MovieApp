using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Movie.Contracts;
using Movie.Core.DomainContracts;
using Movie.Core.DomainEntities;
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

    public async Task<MovieAllDetailsDto> GetMovieDetailsAsync(int id, bool trackChanges = false)
    {
        var movie = await uow.MovieRepository.GetDetailsAsync(id, trackChanges);
        
        if (movie is null) return null!;

        return mapper.Map<MovieAllDetailsDto>(movie);
    }

    public async Task<MovieDto> AddMovieAsync(MovieCreateDto dto, bool trackChanges = false)
    {
        var movieEntity = mapper.Map<Core.DomainEntities.Movie>(dto);
        var movieDetailsEntity = mapper.Map<MovieDetailes>(dto.Detailes);
        movieEntity.Detailes = movieDetailsEntity;

        await uow.MovieRepository.AddAsync(movieEntity);

        await uow.CompleteAsync();

        return mapper.Map<MovieDto>(movieEntity);

    }

    public async Task<MovieDto> DeleteMovieAsync(int id, bool trackChanges = false)
    {
        var movie = await uow.MovieRepository.GetAsync(id, trackChanges);

        if (movie is null)
            return null!;

        var dto = mapper.Map<MovieDto>(movie);

        uow.MovieRepository.Delete(movie);
        await uow.CompleteAsync();

        return dto;
    }

    public async Task<MovieDto> UpdateMovieAsync(int id, MovieUpdateDto dto, bool trackChanges = false)
    {
        var movie = await uow.MovieRepository.GetAsync(id, trackChanges);

        if (movie is null)
            return null!;

        mapper.Map(dto, movie);

        uow.MovieRepository.Update(movie); 
        await uow.CompleteAsync();         
        return mapper.Map<MovieDto>(movie);
    }


    public async Task<MovieDto> PatchMovieAsync(int id, JsonPatchDocument<MoviePatchDto> patchDoc, bool trackChanges = false)
    {
        var movie = await uow.MovieRepository.GetAsync(id, trackChanges);

        if (movie is null)
            return null!;

        var movieToPatch = mapper.Map<MoviePatchDto>(movie);

        patchDoc.ApplyTo(movieToPatch);

        mapper.Map(movieToPatch, movie);

        uow.MovieRepository.Update(movie);
        await uow.CompleteAsync();

        return mapper.Map<MovieDto>(movie);
    }



}
