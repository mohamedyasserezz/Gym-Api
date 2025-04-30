﻿using Gym_Api.Common.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;


namespace Gym_Api.Survices.Authentication
{
    public class EmailService(IOptions<MailSettings> options, ILogger<EmailService> logger) : IEmailSender
    {
        private readonly MailSettings mailSettings = options.Value;
        private readonly ILogger<EmailService> _logger = logger;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(mailSettings.Mail),
                Subject = subject
            };

            message.To.Add(MailboxAddress.Parse(email));

            var builder = new BodyBuilder()
            {
                HtmlBody = htmlMessage
            };

            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            _logger.LogInformation("try to send email to {email}", email);

            client.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            client.Authenticate(mailSettings.Mail, mailSettings.Password);

            await client.SendAsync(message);

            client.Disconnect(true);
        }
    }
}
