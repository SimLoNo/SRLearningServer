using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Data;

namespace SRLearningServer.Components.Utilities
{
    public class EmailSenderUtility : BaseEmailSenderUtility, IEmailSender<ApplicationUser>
    {
        public EmailSenderUtility(IOptions<AuthMessageSenderOptionsUtility> optionsAccessor) : base(optionsAccessor)
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
