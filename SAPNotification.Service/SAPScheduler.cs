using System;
using System.ServiceProcess;
using System.Timers;
using log4net;
using log4net.Config;
using System.Linq;
using SAPNotifications.Engine;
using Core.Repository;
using System.Globalization;

namespace SAPNotification.Service
{
    public partial class SAPScheduler : ServiceBase
    {
        private static Timer timer1 = null;
        protected IUnitOfWork _unitOfWork;
        private ILog log;
        private SAPNotificationEngine notification;

        public SAPScheduler()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(SAPScheduler));
            log.Info("SAP Notification Scheduler Initialized.");
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            string interval = System.Configuration.ConfigurationManager.AppSettings.Get("NotificationInterval");
            timer1.Interval = Convert.ToInt32(interval, CultureInfo.InvariantCulture); // 2 Secs. (default from configuration)
            log.Info("SAP Notification Scheduler Started ." + interval);
            notification = new SAPNotificationEngine();
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            if (notification.IsInProcess() == false)
            {
                Start_Notification_Service();
            }

        }
        private void Start_Notification_Service()
        {
            try
            {
                //log.Info("IPMS SAP Notification Started.");
                notification.Start();
                //log.Info("IPMS SAP Notification Completed");
            }
            catch (Exception ex)
            {
                log.Error("SAP Notification Scheduler error " + ex.Message);
            }
        }
        protected override void OnStop()
        {
            timer1.Enabled = false;
            log.Info("SAP Notification Scheduler stopped..");
        }
    }
}
