using System;

namespace myshop.Web.ViewModels;

public class UserVM
{
    public required string Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
}
