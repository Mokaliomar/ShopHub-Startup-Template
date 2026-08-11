using System;

namespace BusinessLogic.DTOs;

public class ArchivedProductsDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = "";
}
