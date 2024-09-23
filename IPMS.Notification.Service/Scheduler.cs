using System;
using System.Globalization;
using System.ServiceProcess;
using IPMS.Notifications.Engine;
using System.Timers;
using log4net;
using log4net.Config;

namespace IPMSNotificationService
{
    public partial class Scheduler : ServiceBase
    {
        private ILog log;
        private NotificationEngine notification;
        private string notificationMessageId = null;
        private static Timer timer1 = null;

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            string interval = System.Configuration.ConfigurationManager.AppSettings.Get("NotificationInterval");
            timer1.Interval = Convert.ToInt32(interval, CultureInfo.InvariantCulture); // 2 Secs. (default from configuration)
            log.Info("Notification Scheduler OnStart method started."+interval);
            notification = new NotificationEngine();
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            timer1.Enabled = true;
        }
        public Scheduler()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(Scheduler));
            log.Info("Notification Scheduler Initialized.");
        }

        public void Start_Notification_Service()
        {
            try
            {
                //log.Info("IPMS Notification ProcessAllMessages method Started.");
                notification.ProcessAllMessages();
                notification.PushSchedulerJobsMessageToQueue();
                notification.ProcessSAPVesselMasterData();
                //log.Info("IPMS Notification ProcessAllMessages method Completed.");
            }
            catch (Exception ex)
            {
                log.Error("Notification Scheduler error " + ex.Message);
            }
        }
        protected override void OnStop()
        {
            log.Info("Notification Scheduler stopped..");
        }
        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            if (notification.IsInProcess() == false)
            {
                Start_Notification_Service();
            }

        }

    }
}
