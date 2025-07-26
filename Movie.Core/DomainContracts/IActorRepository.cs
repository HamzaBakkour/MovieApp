using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.DomainEntities;

namespace Movie.Core.DomainContracts;


public interface IActorRepository : IRepositoryBase<Actor>
{
    Task<Actor?> GetAsync(int id, bool trackChanges = false);
    Task<List<Actor>> GetAllAsync(bool trackChanges = false);
}