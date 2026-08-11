using System;

namespace BusinessLogic.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public string CreatedTime { get; set; }
}
