namespace SRLearningServer.Components.Interfaces.Utilities
{
    public interface INotificationUtility
    {
        /// <summary>
        /// Sends a regular notification with the given message to the UI.
        /// </summary>
        /// <param name="message"></param>
        public void SendNotification(string message);
        /// <summary>
        /// Sends an error notification with the given message to the UI.
        /// </summary>
        /// <param name="message"></param>
        public void SendErrorNotification(string message);
        /// <summary>
        /// Sends a success notification with the given message to the UI.
        /// </summary>
        /// <param name="message"></param>
        public void SendSuccessNotification(string message);
    }
}
