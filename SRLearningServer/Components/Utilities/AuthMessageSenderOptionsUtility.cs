namespace SRLearningServer.Components.Utilities
{
    public class AuthMessageSenderOptionsUtility
    {
        public string? SMTPServerUrl { get; set; }
        public string? SMTPServerPort { get; set; }
        public string? DefaultEmailSenderName { get; set; }
        public string? EmailPassword { get; set; }
    }
}
