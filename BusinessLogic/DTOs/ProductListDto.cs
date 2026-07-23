using System;

namespace BusinessLogic.DTOs;

public class ProductListDto
{
    public IEnumerable<ProductDto> Items { get; set; } = [];
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public string? SearchTerm { get; set; }
    public string? SortingTerm { get; set; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
}
