using System;
// using System.Net.Mail;
using BusinessLogic.Configurations;
using BusinessLogic.Services.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;


using MailKit.Net.Smtp;
using BusinessLogic.DTOs;
using System.Text;


namespace BusinessLogic.Services.Implementation;

public class MailKitEmailService : IEmailService
{
    private readonly SmtpSettings _settings;
    public MailKitEmailService(IOptions<SmtpSettings> options)
    {
        _settings = options.Value;
    }

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

    public async Task CreateOrderConfirmationEmail(string toName, string toEmail, OrderConfirmationDTO orderInfo)
    {
        var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "OrderConfirmation.html");
        
        var template = File.ReadAllText(templatePath);
        
        var orderTemplate = new StringBuilder(template);
        
        orderTemplate.Replace("{{CustomerName}}", orderInfo.CustomerName);
        orderTemplate.Replace("{{OrderId}}", $"{orderInfo.OrderId}");
        
        // //? Replacing the {{OrderItemsRows}}} with the list of the order items "SOME HOW" !
        var orderItems = new StringBuilder();
        foreach(var item in orderInfo.OrderItems)
        {
            orderItems.Append(@$"
            <tr style='color: #6c757d; text-align: left;'>
                <td style='padding: 8px 0; border-bottom: 1px solid #dee2e6;'>{item.Name}</td>
                <td style='padding: 8px 0; text-align: center; border-bottom: 1px solid #dee2e6;'>{item.Quantity}</td>
                <td style='padding: 8px 0; text-align: right; border-bottom: 1px solid #dee2e6;'>{item.Price}</td>
            </tr>
            ");
        }
        orderTemplate.Replace("{{OrderItemsRows}}", orderItems.ToString());

        orderTemplate.Replace("{{TotalPrice}}", orderInfo.TotalPrice.ToString("C2"));
        orderTemplate.Replace("{{ShippingAddress}}", orderInfo.ShippingAddress);
        orderTemplate.Replace("{{City}}", orderInfo.City);
        orderTemplate.Replace("{{PhoneNumber}}", orderInfo.PhoneNumber);
        orderTemplate.Replace("{{EstimatedDeliveryDate}}", orderInfo.EstimatedDeliveryDate);
        orderTemplate.Replace("{{Year}}", "2026");
        
        EmailData email = new()
        {
            ToName = toName,
            ToEmail = toEmail,
            Subject = "Order Confirmation",
            Content = orderTemplate.ToString()
        };

        await SendEmailAsync(email);
    }
}
