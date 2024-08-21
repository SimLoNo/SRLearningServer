using Radzen;
using SRLearningServer.Components.Interfaces.Utilities;

namespace SRLearningServer.Components.Utilities
{

    
    public class NotificationUtility : INotificationUtility
    {
        private readonly NotificationService _notificationUtility;
        public NotificationUtility(NotificationService notificationUtility)
        {
            _notificationUtility = notificationUtility;
        }
        public void SendErrorNotification(string message)
        {
            NotificationBase(new NotificationMessage {
                Severity = NotificationSeverity.Error,
                Summary = "Fejl",
                Detail = $"{message}",
                Duration = 4000
            });
        }

        public void SendNotification(string message)
        {
            NotificationBase(new NotificationMessage
            {
                Severity = NotificationSeverity.Info,
                Summary = "Notifikation",
                Detail = $"{message}",
                Duration = 4000
            });
        }

        public void SendSuccessNotification(string message)
        {
            NotificationBase(new NotificationMessage { 
                Severity = NotificationSeverity.Success,
                Summary = "Success",
                Detail = $"{message}",
                Duration = 4000 });
        }

        private void NotificationBase(NotificationMessage message)
        {
            _notificationUtility.Notify(message);
        }
    }
}
