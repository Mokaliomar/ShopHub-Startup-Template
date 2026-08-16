using System;

namespace BusinessLogic.DTOs;

public class EmailData
{
    public string FromName { get; set; } = "omarsshop11@gmail.com";
    public string FromEmail { get; set; } = "omarsshop11@gmail.com";
    public string ToName { get; set; } = "";
    public string ToEmail { get; set; } = "";
    public string Subject { get; set; } = "";
    public string Content { get; set; } = "";
}
