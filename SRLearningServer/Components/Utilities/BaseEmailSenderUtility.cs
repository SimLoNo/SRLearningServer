﻿using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Services;

namespace SRLearningServer.Components.Utilities
{
    public class BaseEmailSenderUtility : IBaseEmailSender
    {
        public BaseEmailSenderUtility(IOptions<AuthMessageSenderOptionsUtility> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptionsUtility Options { get; } //set only via Secret Manager
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SMTPServerUrl))
            {
                throw new Exception("Null Server URL");
            }
            if (string.IsNullOrEmpty(Options.SMTPServerPort))
            {
                throw new Exception("Null Server Port");
            }
            if (string.IsNullOrEmpty(Options.DefaultEmailSenderName))
            {
                throw new Exception("Null Email");
            }
            if (string.IsNullOrEmpty(Options.EmailPassword))
            {
                throw new Exception("Null Password");
            }

            await Execute(Options.SMTPServerUrl, Options.SMTPServerPort, Options.DefaultEmailSenderName, Options.EmailPassword, subject, message, toEmail);
        }

        public async Task Execute(string url, string port, string email, string password, string subject, string message, string toEmail)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("SR Learning", email));
            mailMessage.To.Add(new MailboxAddress(toEmail, toEmail));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("html")
            {
                Text = message
            };

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(url, Convert.ToInt32(port), true);
                await smtpClient.AuthenticateAsync(email, password);
                await smtpClient.SendAsync(mailMessage);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
