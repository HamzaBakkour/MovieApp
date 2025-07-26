using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Movie.Contracts;
using Movie.Core.DomainContracts;
using Movie.Core.DomainEntities;
using Movie.Core.Dtos;
using Movie.Core.Exceptions;

namespace Movie.Services;

public class ActorService : IActorService
{
    private IUnitOfWork uow;
    private readonly IMapper mapper;


    public ActorService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }


    private void BusinessRulesValidation(Core.DomainEntities.Movie movie, Actor actor)
    {

        /// "Actor is already assigned to this movie."
        if (movie.Actors.Any(a => a.Id == actor.Id))
            throw new ActorAlreayAssignedException(actor.Id);

        /// a Documentary movie can not have more than 10 actors
        bool isDocumentary = movie.Genres.Any(g => string.Equals(g.Name, "Documentary", StringComparison.OrdinalIgnoreCase));
        if (isDocumentary && movie.Actors.Count >= 10)
            throw new MovieExceededMaxActorsException(movie.Id, 10, "Documentary");

    }



    public async Task<IEnumerable<ActorDto>> GetActorsAsync(bool trackChanges = false)
    {
        return mapper.Map<IEnumerable<ActorDto>>(await uow.ActorRepository.GetAllAsync(trackChanges));
    }


    public async Task<ActorDto> AddActorAsync(ActorCreateDto dto, bool trackChanges = false)
    {
        var actorEntity = mapper.Map<Actor>(dto);

        await uow.ActorRepository.AddAsync(actorEntity);

        await uow.CompleteAsync();

        return mapper.Map<ActorDto>(actorEntity);

    }



    public async Task<ActorDto> AddActorToMovieAsync(int movieId, int actorId,bool trackChanges = true)
    {
        var movie = await uow.MovieRepository.GetDetailsAsync(movieId, trackChanges);
        if (movie is null) throw new MovieNotFoundException(movieId);

        var actor = await uow.ActorRepository.GetAsync(actorId, trackChanges);
        if (actor is null) throw new ActorNotFoundException(actorId);

        BusinessRulesValidation(movie, actor);

        movie.Actors.Add(actor);

        uow.MovieRepository.Update(movie);

        await uow.CompleteAsync();

        return mapper.Map<ActorDto>(actor);

    }



}
