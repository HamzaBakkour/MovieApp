using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.Dtos;

namespace Movie.Core.DomainContracts;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    //void Create(T entity);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<PagedResult<T>> GetPagedAsync(MoviePagingParametersDto parameters, bool trackChanges = false);

}
