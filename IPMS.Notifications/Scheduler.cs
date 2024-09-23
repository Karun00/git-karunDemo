using System;
using System.ServiceProcess;
using System.Timers;
using IPMS.Data.Context;
using Core.Repository;
using log4net;
using log4net.Config;

namespace IPMS.Notifications
{
    public partial class Scheduler : ServiceBase
    {
        private Timer timer1 = null;
        protected IUnitOfWork _unitOfWork;
        private ILog log;
        private NotificationEngine notification;

        public Scheduler()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(Scheduler));
            log.Info("Notification Scheduler Initialized.");
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            string interval = System.Configuration.ConfigurationManager.AppSettings.Get("NotificationInterval");
            this.timer1.Interval = Convert.ToInt32(interval); // 2 Secs. (default from configuration)
            log.Info("Notification Started ." + interval);
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);

            _unitOfWork = new UnitOfWork(new TnpaContext());
            notification = new NotificationEngine(_unitOfWork);

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            Start_Notification_Service();

        }
        private void Start_Notification_Service()
        {
            try
            {

                log.Info("IPMS Notification Started.");
                notification.Start();
                log.Info("IPMS Notification Completed");

            }
            catch (Exception ex)
            {
                log.Error("IPMS Notification window service error " + ex.Message);
            }
            //finally
            //{
            //    if (_unitOfWork != null)
            //    {
            //        _unitOfWork.Dispose();
            //    }
            //}

        }
        protected override void OnStop()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
            timer1.Enabled = false;
            log.Info("IPMS Notification window service stopped..");
        }
    }
}
