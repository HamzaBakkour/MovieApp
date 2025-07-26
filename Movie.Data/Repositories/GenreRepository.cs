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

public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
{

    public GenreRepository(ApplicationDbContext context) : base(context) { }


    public async Task<List<Genre>> GetAllAsync(bool trackChanges = false) =>
                                            await FindAll(trackChanges).ToListAsync();
}
