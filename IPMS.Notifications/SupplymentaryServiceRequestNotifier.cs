using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using log4net;
using System;
using log4net.Config;

namespace IPMS.Notifications
{
    public class SupplymentaryServiceRequestNotifier : Notifier
    {
        private SuppServiceRequestVO _supplymentaryServiceRequest;
        protected ISupplymentaryServiceRepository _supplymentaryServiceRequestRepositoy;

        public SupplymentaryServiceRequestNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(SupplymentaryServiceRequestNotifier));
                _supplymentaryServiceRequestRepositoy = new SupplymentaryServiceRepository(_NotifierunitOfWork);
                _supplymentaryServiceRequest = _supplymentaryServiceRequestRepositoy.GetSuppServiceRequestByID(Convert.ToInt32(pendingNotification.Reference));
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
            return _userRepository.GetUsersForRoleAndPortCodeByUserType(GetPortCode(), this.roles, pendingNotification.UserType, pendingNotification.UserTypeId);
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _supplymentaryServiceRequest);
        }
    }
}
