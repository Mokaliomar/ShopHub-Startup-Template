using System;
using System.Linq.Expressions;
using DataAccess.Data;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    protected DbSet<T> dbSet;
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        dbSet = _context.Set<T>();
    }

    public virtual IEnumerable<T> All()
    {
        return dbSet.ToList();
    }
    public virtual T GetById(int? Id)
    {
        return dbSet.Find(Id)!;
    }

    public async Task<T> GetWithIgnoreFiltersAsync(Expression<Func<T, bool>> predicate)
    {
        return await dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(predicate);
    }

    public virtual void Create(T entity)
    {
        dbSet.Add(entity);
    }

    public virtual void Delete(int? Id)
    {
        dbSet.Remove(GetById(Id));
    }

    public virtual void Update(T entity)
    {
        dbSet.Update(entity);
    }
}
