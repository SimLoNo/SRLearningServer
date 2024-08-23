using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Data;

namespace SRLearningServer.Components.Services
{
    public class EmailSender : BaseEmailSender, IEmailSender<ApplicationUser>
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor) : base(optionsAccessor)
        {

        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) => SendEmailAsync(email, "Confirm your email",
        "Please confirm your account by " +
        $"<a href='{confirmationLink}'>clicking here</a>.");
        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) => SendEmailAsync(email, "Reset your password", 
        $"Please reset your password using the following code: {resetCode}");



        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) => SendEmailAsync(email, "Reset your password",
        $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");


        
    }

}
