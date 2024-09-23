using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;


namespace IPMS.Notifications
{
    public class VesselRegistrationNotifier : Notifier
    {
        private Vessel _vesselRegistration;
        protected IVesselRegistrationRepository _vesselRegistrationRepositoy;

        public VesselRegistrationNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(VesselRegistrationNotifier));
                _vesselRegistrationRepositoy = new VesselRegistrationRepository(_NotifierunitOfWork);
                _vesselRegistration = _vesselRegistrationRepositoy.GetVesselRegistrationByIMO(pendingNotification.Reference);
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
            List<User> _userslist = new List<User>();
            List<NotificationRole> _roleslist = new List<NotificationRole>();

            int _usertypeid;
            if (Convert.ToInt32(_vesselRegistration.ModifiedBy) == 0)
                _usertypeid = _vesselRegistration.CreatedBy;
            else
                _usertypeid = Convert.ToInt32(_vesselRegistration.ModifiedBy);

            var usertype = _NotifierunitOfWork.Repository<User>().Find(_usertypeid).UserType;

            foreach (var role in this.roles)
            {
                if (role.Role.RoleCode == Roles.Agent && usertype == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                {
                    _userslist.Add(_userRepository.GetUserById(_usertypeid));
                }
                else if (role.Role.RoleCode == Roles.TerminalOperator && usertype == Roles.TerminalOperator) //Assumption : UserType.TerminalOperator and TerminalOperator role code('TO') should be same 
                {
                    _userslist.Add(_userRepository.GetUserById(_usertypeid));
                }
                else
                {
                    _roleslist = new List<NotificationRole>();
                    _roleslist.Add(role);
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode,0));
                }
            }
            return _userslist;
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _vesselRegistration);
        }
    }
}
