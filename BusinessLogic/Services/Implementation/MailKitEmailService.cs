using System;
// using System.Net.Mail;
using BusinessLogic.Configurations;
using BusinessLogic.Services.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;


using MailKit.Net.Smtp;
using BusinessLogic.DTOs;


namespace BusinessLogic.Services.Implementation;

public class MailKitEmailService : IEmailService
{
    private readonly SmtpSettings _settings;
    public MailKitEmailService(IOptions<SmtpSettings> options)
    {
        _settings = options.Value;
    }

    //? I think we will make an Email Class so we can handle the parameters more easily
    //! Stopped at .. why i can't make it use Asynchronous ???
    public async Task SendEmailAsync(EmailData email)
    {
        //* Making the Mail
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(email.FromName, email.FromEmail));

        message.To.Add(new MailboxAddress(email.ToName, email.ToEmail));

        message.Subject = email.Subject;

        message.Body = new TextPart("html")
        {
            Text = email.Content
        };

        //* Sending the Mail
        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.SslOnConnect);

                await client.AuthenticateAsync(_settings.SmtpUserName, _settings.SmtpPassword);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);

                System.Console.WriteLine("Sent Successfully");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error " + ex.Message);
            }
        }

    }

    public async Task CreateEmailConfirmationMessageAsync(string toName, string toEmail, string confirmationLink, string year)
    {
        var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "EmailConfirmation.html");
        var emailContent = await File.ReadAllTextAsync(templatePath);

        emailContent = emailContent.Replace("{{UserName}}", toName);
        emailContent = emailContent.Replace("{{ConfirmationLink}}", confirmationLink);
        emailContent = emailContent.Replace("{{Year}}", year);

        EmailData email = new EmailData()
        {
            ToName = toName,
            ToEmail = toEmail,
            Subject = "Confirmation Message",
            Content = emailContent
        };

        await SendEmailAsync(email);
    }

}
