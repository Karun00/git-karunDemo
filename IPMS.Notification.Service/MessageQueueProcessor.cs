using System;
using IPMS.Notifications.Engine;
using log4net;
using log4net.Config;

namespace IPMSNotificationService
{
    public class MessageQueueProcessor
    {
        public void ProcessMessage(string messageId)
        {
            ILog log;
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(MessageQueueProcessor));
            if (!string.IsNullOrEmpty(messageId))
            {
                log.Info("ProcessMessage method Initialized.");
                log.Info("Notification Templatecode is : " + messageId);
                try
                {
                    NotificationEngine notification = new NotificationEngine();
                    notification.ProcessAllMessages();//(messageId);
                        log.Info("ProcessMessage method Completed.");
                }
                catch (Exception ex)
                {
                    log.Error("Error : " + ex.Message);
                }
            }
            else
            {
                log.Info("Queue name is empty");
            }
        }
    }
}
