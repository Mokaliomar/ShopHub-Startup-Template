using System;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

//* Used for a specific methods for the Category class only
//* Since we don't have a specific thing for the Category class, so it will be empty
public interface ICategoryRepository : IGenericRepository<Category>
{}
