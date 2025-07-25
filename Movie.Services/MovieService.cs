﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Movie.Contracts;
using Movie.Core;
using Movie.Core.DomainContracts;
using Movie.Core.DomainEntities;
using Movie.Core.Dtos;
using Movie.Core.Exceptions;

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

        if (movie is null) throw new MovieNotFoundException(id);

        return mapper.Map<MovieDto>(movie);
    }


    public async Task<IEnumerable<MovieDto>> GetMoviesAsync(bool trackChanges = false)
    {
        return mapper.Map<IEnumerable<MovieDto>>(await uow.MovieRepository.GetAllAsync(trackChanges));
    }


    public async Task<MovieAllDetailsDto> GetMovieDetailsAsync(int id, bool trackChanges = false)
    {
        var movie = await uow.MovieRepository.GetDetailsAsync(id, trackChanges);

        if (movie is null) throw new MovieNotFoundException(id);

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

        if (movie is null) throw new MovieNotFoundException(id);

        var dto = mapper.Map<MovieDto>(movie);

        uow.MovieRepository.Delete(movie);
        await uow.CompleteAsync();

        return dto;
    }


    public async Task<MovieDto> UpdateMovieAsync(int id, MovieUpdateDto dto, bool trackChanges = false)
    {
        var movie = await uow.MovieRepository.GetAsync(id, trackChanges);

        if (movie is null) throw new MovieNotFoundException(id);

        mapper.Map(dto, movie);

        uow.MovieRepository.Update(movie);
        await uow.CompleteAsync();
        return mapper.Map<MovieDto>(movie);
    }


    public async Task<MovieAllDetailsDto> PatchMovieAsync(int id, JsonPatchDocument<MoviePatchDto> patchDoc, bool trackChanges = false)
    {
        var movie = await uow.MovieRepository.GetDetailsAsync(id, trackChanges);
        if (movie is null) throw new MovieNotFoundException(id);

        var movieToPatch = mapper.Map<MoviePatchDto>(movie);

        movieToPatch.Detailes ??= new MovieDetailesPatchDto();

        patchDoc.ApplyTo(movieToPatch);

        mapper.Map(movieToPatch, movie);

        if (movieToPatch.Detailes is not null && movie.Detailes is not null)
        {
            mapper.Map(movieToPatch.Detailes, movie.Detailes);
        }

        uow.MovieRepository.Update(movie);
        await uow.CompleteAsync();

        return mapper.Map<MovieAllDetailsDto>(movie);
    }


    public async Task<PagedResult<MovieDto>> GetMoviesAsync(MoviePagingParametersDto parameters, bool trackChanges = false)
    {
        var pagedMovies = await uow.MovieRepository.GetPagedAsync(parameters, trackChanges);

        var movieDtos = mapper.Map<List<MovieDto>>(pagedMovies);

        return new PagedResult<MovieDto>(
            movieDtos,
            pagedMovies.TotalCount,
            pagedMovies.CurrentPage,
            pagedMovies.PageSize
        );
    }


}
