using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using log4net;
using SAPNotifications.Engine;
using System.Globalization;

namespace SAPNotification.Console
{
    public class SAPScheduler
    {
        private Timer timer1 = null;
        private ILog log;
        private SAPNotificationEngine sapnotification;

        public SAPScheduler()
        {
            log = LogManager.GetLogger(typeof(SAPScheduler));
            log.Info("SAP Notification Scheduler Initialized.");
        }

        public void OnStart()
        {
            timer1 = new Timer();
            string interval = System.Configuration.ConfigurationManager.AppSettings.Get("NotificationInterval");
            this.timer1.Interval = Convert.ToInt32(interval, CultureInfo.InvariantCulture); // 2 Secs. (default from configuration)
            log.Info("SAP Notification Started ." + interval);
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            sapnotification = new SAPNotificationEngine();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            if (sapnotification.IsInProcess() == false)
            {
                Start_Notification_Service();
            }
        }
        public void Start_Notification_Service()
        {
            try
            {
                log.Info("IPMS SAP Notification Started.");
                sapnotification.Start();
                log.Info("IPMS SAP Notification Completed");

            }
            catch (Exception ex)
            {
                log.Error("IPMS SAP Notification window service error " + ex.Message);
            }
        }
        public void OnStop()
        {
            timer1.Enabled = false;
            log.Info("IPMS SAP Notification window service stopped..");
        }


    }
}
