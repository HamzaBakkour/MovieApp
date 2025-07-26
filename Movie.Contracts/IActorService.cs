using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.DomainEntities;
using Movie.Core.Dtos;

namespace Movie.Contracts;

public interface IActorService
{
    
    Task<IEnumerable<ActorDto>> GetActorsAsync(bool trackChanges = false);
    Task<ActorDto> AddActorAsync(ActorCreateDto dto, bool trackChanges = false);
    Task<ActorDto> AddActorToMovieAsync(int movieId, int actorId, bool trackChanges = false);
}
