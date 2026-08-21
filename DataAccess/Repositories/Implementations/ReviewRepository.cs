using System;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context) : base(context)
    {}

    public IQueryable<Review> GetProductReviews(int? productId)
    {
        var productReviews = dbSet.Include(review => review.Product)
                                    .Where(review => review.ProductId == productId);
        return productReviews;
    }
}
