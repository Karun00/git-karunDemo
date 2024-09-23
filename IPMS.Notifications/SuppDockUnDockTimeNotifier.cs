using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using log4net;
using System.Collections.Generic;
using System;
using log4net.Config;

namespace IPMS.Notifications
{
    public class SuppDockUnDockTimeNotifier : Notifier
    {
        private SuppDryDock _suppDockUnDockTime;
        protected ISuppDockUnDockTimeRepository _suppdockundockRepository;

        public SuppDockUnDockTimeNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(SuppDockUnDockTimeNotifier));
                _suppdockundockRepository = new SuppDockUnDockTimeRepository(_NotifierunitOfWork);
                _suppDockUnDockTime = _suppdockundockRepository.GetSuppDockUndockDetailsByID(pendingNotification.Reference);
            }
            catch (Exception ex)
            {
                log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            }

        }

        public override string GetPortCode()
        {
            return pendingNotification.PortCode;
        }

        public override List<User> GetUsersToBeNotified()
        {
            return _userRepository.GetUsersForRoleAndPortCode(GetPortCode(), this.roles);
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _suppDockUnDockTime);
        }

    }
}
