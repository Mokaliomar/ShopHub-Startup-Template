using System;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs;
using Microsoft.Extensions.Options;

namespace BusinessLogic.Services.Interfaces;

public interface IEmailService
{
    // Task SendEmailAsync(string fromName, string fromEmail, string toName, string toEmail, string subject, string content);
    Task SendEmailAsync(EmailData email);
    Task CreateEmailConfirmationMessageAsync(string toName, string toEmail, string confirmationLink, string year);
}
