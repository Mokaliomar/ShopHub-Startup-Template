using System;
using BusinessLogic.DTOs;
using DataAccess.Models;
using Mapster;

namespace BusinessLogic.Configurations;

public static class MapsterConfig
{
    public static void RegisterConfig()
    {
        TypeAdapterConfig<Product, ProductDto>.NewConfig()
            .Map(dest => dest.Image, src => src.Img)
            .Map(dest => dest.CategoryName, src => src.Category.Name)
            .Map(dest => dest.ReviewsCount, src => src.ProductReviews.Count)
            .Map(dest => dest.AverageRate, src => src.ProductReviews.Any()
                                                ? src.ProductReviews.Average(pr => pr.ProductRate)
                                                : 0);
    }
}
