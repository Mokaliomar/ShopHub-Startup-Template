using System;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IReviewRepository : IGenericRepository<Review>
{
    IQueryable<Review> GetProductReviews(int? productId);
}
