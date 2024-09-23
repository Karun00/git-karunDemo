using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Repository;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IPMS.Notifications
{
    public class NotificationEngine
    {
        protected IUnitOfWork _unitOfWork = null;
        private INotificationRepository _notificationRepository;
        private ILog log;
        public EmailSender emailSender;
        public SMSSender smsSender;

        public NotificationEngine(IUnitOfWork unitofwork)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(NotificationEngine));
                _unitOfWork = unitofwork;
                _notificationRepository = new NotificationRepository(_unitOfWork);
                emailSender = new EmailSender();
                smsSender = new SMSSender();
                
            }
            catch (Exception ex)
            {
                log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            }
        }

        public void Start()
        {
            try
            {
                var pendingNotifications = _notificationRepository.GetPendingNotifications();
                if (pendingNotifications != null)
                {
                    ProcessNotifications(pendingNotifications);
                }
            }
            catch (Exception ex)
            {
                log.Error("NotificationEngine Start() method Error : " + ex.Message);
            }
        }

        private void ProcessNotifications(List<Notification> pendingNotifications)
        {

            foreach (Notification pendingNotification in pendingNotifications)
            {
                Stopwatch wfstopwatch;
                wfstopwatch = Stopwatch.StartNew();
                log.Info(" Pending Notification ID : " + pendingNotification.NotificationId + " Started");

                try
                {
                    Notifier notifier = Notifier.GetNotifier(pendingNotification);
                    try
                    {
                        log.Debug(pendingNotification.NotificationTemplateCode + " Process started...");

                        notifier.SetEmailSMSObject(emailSender, smsSender);

                        bool sts = notifier.ProcessNotification();

                        log.Debug(pendingNotification.NotificationTemplateCode + " Process completed...");
                    }
                    catch (Exception ex)
                    {

                        log.Error("ProcessNotifications : " + ex.Message);
                    }
                }
                catch (Exception ex)
                {

                    log.Error("ProcessNotifications Failed : " + ex.Message);
                }
                wfstopwatch.Stop();
                log.Info("Pending Notification ID : " + pendingNotification.NotificationId + " Completed in " + wfstopwatch.Elapsed.ToString());

            }
        }
    }
}