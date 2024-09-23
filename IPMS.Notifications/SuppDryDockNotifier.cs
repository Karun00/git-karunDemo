using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using Core.Repository;
using IPMS.Data.Context;
using log4net;
using System;
using log4net.Config;

namespace IPMS.Notifications
{
    public class SuppDryDockNotifier : Notifier
    {
        private SuppDryDock _suppDryDock;
        protected ISuppDryDockRepository _suppDryDockRepository;

        public SuppDryDockNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {         
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(DockingPlanNotifier));
                _suppDryDockRepository = new SuppDryDockRepository(_NotifierunitOfWork);
                _suppDryDock = _suppDryDockRepository.GetSuppDryDockRequestDetailsByID(pendingNotification.Reference);
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
            return Common.GetTokensDictionary(entityDetails, _suppDryDock);
        }
    }
}
