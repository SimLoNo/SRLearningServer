namespace SRLearningServer.Components.Interfaces.Services
{
    public interface IBaseEmailSender
    {
        public Task SendEmailAsync(string toEmail, string subject, string message);
        public Task Execute(string url, string port, string email, string password, string subject, string message, string toEmail);
    }
}
