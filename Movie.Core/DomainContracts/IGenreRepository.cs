﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.DomainEntities;

namespace Movie.Core.DomainContracts;

public interface IGenreRepository : IRepositoryBase<Genre>
{
    Task<List<Genre>> GetAllAsync(bool trackChanges = false);
}
