using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Services.WorkFlow;
using log4net;
using System;
using System.Collections.Generic;
using IPMS.Services;

namespace IPMS.Notifications
{
    public class VesselArrestImmobilizationSAMSAStopNotifier : Notifier
    {
        private VesselArrestImmobilizationSAMSAStopVO _vesselArrests;
        protected IVesselArrestImmobilizationSAMSAStopService _vesselArrestImmobilizationSAMSAStopService;
        public VesselArrestImmobilizationSAMSAStopNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                log = LogManager.GetLogger(typeof(VesselArrestImmobilizationSAMSAStopNotifier));
               _vesselArrests = _vesselArrestImmobilizationSAMSAStopService.GetVesselArrestImmobilizationSAMSAStopbyID(_vesselArrests.VAISID);
               //_vesselArrests = _vesselArrestList;
              
               // _user.PWD = Password.Decrypt(_user.PWD, true);
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

            if (notificationTemplate.NotificationTemplateBase == "R")
                return _userRepository.GetUsersForRoleAndPortCodeByUserType(GetPortCode(), this.roles, pendingNotification.UserType, pendingNotification.UserTypeId);
            else
            {
                List<User> _users = new List<User>();
                _users.Add(_userRepository.GetUserById(Convert.ToInt32(pendingNotification.Reference)));
                return _users;
            }
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _vesselArrests);
        }

    }
}
