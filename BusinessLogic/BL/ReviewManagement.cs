using System;
using System.Diagnostics;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.BL;

public class ReviewManagement
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewManagement(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<Review> GetProductReviews(int? productId)
    {
        return _unitOfWork.ReviewRepository.GetProductReviews(productId);
    }

    public float GetAvgProductReviewRate(int? productId)
    {
        if (productId is null)
            return 0;

        var reviews = _unitOfWork.ReviewRepository.GetProductReviews(productId);
        
        var reviewsCount = GetProductReviewsCount(productId);
        if(reviewsCount == 0)
            return 0;

        var avgRate = reviews.Sum(review => review.ProductRate) / reviewsCount;
        return avgRate;
    }
    public int GetProductReviewsCount(int? productId)
    {
        var reviewsCount = _unitOfWork.ReviewRepository.GetProductReviews(productId).Count();
        return reviewsCount;
    }

    public Review GetCustomerReview(string? customerId)
    {
        /* var review = _unitOfWork.ReviewRepository.GetById(customerId);
        return review; */
        throw new NotImplementedException();
    }

    public Review GetReviewById(int reviewId)
    {
        return _unitOfWork.ReviewRepository.GetById(reviewId);
    }

    public bool HasReview(string userId, int productId)
    {
        return _unitOfWork.ReviewRepository
                    .GetProductReviews(productId)
                    .Any(review => review.ApplicationUserId == userId);
    }
    public bool AddReview(Review review)
    {
        try
        {
            review.CreatedAt = DateTime.UtcNow;
            _unitOfWork.ReviewRepository.Create(review);
            _unitOfWork.Save();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

    public bool RemoveReview(int reviewId)
    {
        try
        {
            _unitOfWork.ReviewRepository.Delete(reviewId);
            _unitOfWork.Save();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

    public bool EditReview(Review review)
    {
        try
        {
            review.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.ReviewRepository.Update(review);
            _unitOfWork.Save();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

}
