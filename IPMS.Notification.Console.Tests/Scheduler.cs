using System;
using System.Configuration;
using System.Timers;
using IPMS.Notifications.Engine;
using log4net;
using log4net.Config;

namespace IPMS.Notification.Console
{
    public class Scheduler
    {
        private Timer timer1 = null;
        private ILog log;
        private NotificationEngine notification;
        public Scheduler()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(Scheduler));
            log.Info("Notification Scheduler Initialized.");
            notification = new NotificationEngine();
            Start_Notification_Service();
        }

        public void OnStart()
        {
            string interval = ConfigurationManager.AppSettings.Get("NotificationInterval");
            notification = new NotificationEngine();
             Start_Notification_Service();
        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            if (notification.IsInProcess() == false)
            {
                Start_Notification_Service();
            }
        }
        public void Start_Notification_Service()
        {
            try
            {
                log.Info("IPMS Notification Started.");
                notification.ProcessAllMessages();
                notification.PushSchedulerJobsMessageToQueue();
                notification.ProcessSAPVesselMasterData();
                log.Info("IPMS Notification Completed");

            }
            catch (Exception ex)
            {
                log.Error("IPMS Notification window service error " + ex.Message);
            }
        }
        public  void OnStop()
        {
            log.Info("IPMS Notification window service stopped..");
        }


    }
}
