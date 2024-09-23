using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Notifications
{
    public class PilotExemptionRequestNotifier : Notifier
    {
        protected IPilotExemptionRequestRepository _pilotExemptionRequestRepository;

        private Pilot _licenserwequest;

        public PilotExemptionRequestNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(PilotExemptionRequestNotifier));
                _pilotExemptionRequestRepository = new PilotExemptionRequestRepository(_NotifierunitOfWork);
                _licenserwequest = _pilotExemptionRequestRepository.GetPilotRequestDetailsByID(Convert.ToInt32(pendingNotification.Reference));
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
            return Common.GetTokensDictionary(entityDetails, _licenserwequest);
        }
    }
}

