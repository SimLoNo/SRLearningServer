namespace SRLearningServer.Components.Services
{
    public class AuthMessageSenderOptions
    {
        public string? SMTPServerUrl { get; set; }
        public string? SMTPServerPort { get; set; }
        public string? EmailName { get; set; }
        public string? EmailPassword { get; set; }
    }
}
