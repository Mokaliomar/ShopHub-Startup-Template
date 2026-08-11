using System;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> All();
    T GetById(int? Id);
    Task<T> GetWithIgnoreFiltersAsync(Expression<Func<T, bool>> predicate);
    void Create(T entity);
    void Update(T entity);
    void Delete(int? Id);
}
