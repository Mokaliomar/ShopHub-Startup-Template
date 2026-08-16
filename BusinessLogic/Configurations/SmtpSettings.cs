using System;

namespace BusinessLogic.Configurations;

public class SmtpSettings
{
    public string SmtpServer { get; set; } = "";
    public int SmtpPort { get; set; }
    public string SmtpUserName { get; set; } = "";
    public string SmtpPassword { get; set; } = "";
}
