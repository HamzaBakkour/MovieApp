using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie.Core.DomainContracts;
using Movie.Core.DomainEntities;
using Movie.Data.DataConfigurations;

namespace Movie.Data.Repositories;



public class ActorRepository : RepositoryBase<Actor>, IActorRepository
{
    public ActorRepository(ApplicationDbContext context) : base(context) { }


    public async Task<Actor?> GetAsync(int id, bool trackChanges = false) =>
                                                        await FindByCondition(a => a.Id == id, trackChanges).FirstOrDefaultAsync();

    public async Task<List<Actor>> GetAllAsync(bool trackChanges = false) =>
                                                        await FindAll(trackChanges).ToListAsync();


}
