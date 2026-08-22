using System;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context) : base(context)
    { }

    public IQueryable<Review> GetProductReviews(int? productId)
    {
        var productReviews = dbSet.Include(review => review.Product)
                                    .Include(review => review.ApplicationUser)
                                    .Where(review => review.ProductId == productId);
        return productReviews;
    }

    public override Review GetById(int? Id)
    {
        var review = dbSet.Include(review => review.Product)
            .Include(review => review.ApplicationUser)
            .FirstOrDefault(review => review.Id == Id);
        return review;
    }
}
