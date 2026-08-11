using System;
using System.Security.AccessControl;

namespace BusinessLogic.DTOs;

public class ArchivedCategoriesDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string CreatedTime { get; set; } = "";
}
