namespace SRLearningServer.Components.Interfaces.Utilities
{
    public interface INotificationUtility
    {
        public void SendNotification(string message);
        public void SendErrorNotification(string message);
        public void SendSuccessNotification(string message);
    }
}
