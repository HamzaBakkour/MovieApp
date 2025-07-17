using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie.Core;
using Movie.Core.DomainContracts;
using Movie.Core.Dtos;
using Movie.Data.DataConfigurations;

namespace Movie.Data.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected DbSet<T> DbSet { get; }

    public RepositoryBase(ApplicationDbContext context)
    {
        // Context = context;
        DbSet = context.Set<T>();
    }
    public IQueryable<T> FindAll(bool trackChanges = false) =>
        !trackChanges ? DbSet.AsNoTracking() :
                        DbSet;
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
        !trackChanges ? DbSet.Where(expression).AsNoTracking() :
                        DbSet.Where(expression);
    public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);
    public void Update(T entity) => DbSet.Update(entity);
    public void Delete(T entity) => DbSet.Remove(entity);
    public async Task<PagedResult<T>> GetPagedAsync(MoviePagingParametersDto parameters, bool trackChanges = false)
    {
        return await PagedResult<T>.ToPagedListAsync(
            FindAll(trackChanges),
            parameters.PageNumber,
            parameters.PageSize
        );
    }
}
