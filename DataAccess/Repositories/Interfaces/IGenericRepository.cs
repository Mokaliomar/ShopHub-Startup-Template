using System;

namespace DataAccess.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> All();
    T GetById(int? Id);
    void Create(T entity);
    void Update(T entity);
    void Delete(int? Id);
}
