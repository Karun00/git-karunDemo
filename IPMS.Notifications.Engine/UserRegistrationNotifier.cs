using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Services.WorkFlow;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IPMS.Notifications.Engine
{
    public class UserRegistrationNotifier : Notifier
    {
        private UserMasterVO _userRegistration;

        public UserRegistrationNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                log = LogManager.GetLogger(typeof(UserRegistrationNotifier));
                _userRegistration = _userRepository.GetUserByID(Convert.ToInt32(pendingNotification.Reference, CultureInfo.InvariantCulture));
                _userRegistration.PortCode = pendingNotification.PortCode;
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
            if (notificationTemplate.NotificationTemplateBase == "U")
            {
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", Convert.ToInt32(pendingNotification.Reference, CultureInfo.InvariantCulture)));
            }
            else //TODO:Info Email Templates to be defined, hence every email has to send Creator
            {
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", Convert.ToInt32(pendingNotification.Reference, CultureInfo.InvariantCulture)));
            }

            //if (notificationTemplate.NotificationTemplateBase == "U")
            //{
            //    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), pendingNotification.UserType, "", pendingNotification.UserTypeId));
            //}
            //else
            //{
            foreach (var role in this.roles)
            {
                if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                {
                    //NOT REQUIRED
                }
                else if (role.Role.RoleCode == Roles.TerminalOperator) //Assumption : UserType.TO and Agent role code('TO') should be same 
                {
                    //NOT REQUIRED
                }
                else
                {
                    _roleslist = new List<NotificationRole>();
                    _roleslist.Add(role);
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
                }
            }
            //}
            return _userslist.GroupBy(x => x.UserID).Select(x => x.First()).ToList();
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _userRegistration);
        }

    }
}
