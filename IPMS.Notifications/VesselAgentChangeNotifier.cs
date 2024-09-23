using IPMS.Domain.Models;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;


namespace IPMS.Notifications
{
    public class VesselAgentChangeNotifier : Notifier
    {
        private VesselAgentChange _vesselAgentChangeNotification;
        protected IVesselAgentChangeRepository _vesselAgentChangeRepositoy;

        public VesselAgentChangeNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(VesselAgentChangeNotifier));
                _vesselAgentChangeRepositoy = new VesselAgentChangeRepository(_NotifierunitOfWork);
                _vesselAgentChangeNotification = _vesselAgentChangeRepositoy.GetVesselAgentcahngeNotificationByID(Convert.ToString(_vesselAgentChangeNotification.VesselAgentChangeID));
            }
            catch (Exception ex)
            {
                log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            }
        }

        public override string GetPortCode()
        {
            return _vesselAgentChangeNotification.ArrivalNotification.PortCode;
        }

        public override List<User> GetUsersToBeNotified()
        {

            List<User> _users = new List<User>();

            if (notificationTemplate.NotificationTemplateBase == "R")
            {
                _users = _userRepository.GetUsersForRoleAndPortCodeByUserType(GetPortCode(), this.roles, pendingNotification.UserType, pendingNotification.UserTypeId);
                //Sending all Notification emails to Created User (Agent)
                _users.Add(_userRepository.GetUserById(Convert.ToInt32(pendingNotification.CreatedBy)));
            }
            else
            {
                _users.Add(_userRepository.GetUserById(pendingNotification.CreatedBy));
            }

            return _users;
            //return _userRepository.GetUsersForRoleAndPortCode(GetPortCode(), this.roles);
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _vesselAgentChangeNotification);
        }

    }
}
