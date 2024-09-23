using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using IPMS.Repository;
using log4net;
using log4net.Config;

namespace IPMS.Notifications
{
    public class BerthMaintenanceCompletionNotifier : Notifier
    {
          private BerthMaintenanceCompletion _berthMaintenanceCompletion;
        protected IBerthMaintenanceCompletionRepository _berthMaintenanceCompletionRepository;

          public BerthMaintenanceCompletionNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(BerthMaintenanceCompletionNotifier));
                _berthMaintenanceCompletionRepository = new BerthMaintenanceCompletionRepository(_NotifierunitOfWork);
            _berthMaintenanceCompletion = _berthMaintenanceCompletionRepository.GetBerthMaintenanceCompletionDetailsByID(pendingNotification.Reference);
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
            return Common.GetTokensDictionary(entityDetails, _berthMaintenanceCompletion);
        }
    }
}
