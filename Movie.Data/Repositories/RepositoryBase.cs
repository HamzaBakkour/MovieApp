using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie.Core.DomainContracts;
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

    public void Create(T entity) => DbSet.Add(entity);

    public void Update(T entity) => DbSet.Update(entity);

    public void Delete(T entity) => DbSet.Remove(entity);

}
