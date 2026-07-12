using System;
using DataAccess.Data;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    public IProductRepository ProductRepository { get; private set; }

    public ICategoryRepository CategoryRepository { get; private set; }
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        ProductRepository = new ProductRepository(context);
        CategoryRepository = new CategoryRepository(context);
    }


    public int Save()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
