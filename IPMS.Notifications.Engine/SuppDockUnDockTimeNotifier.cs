using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using log4net;
using System.Collections.Generic;
using System;
using log4net.Config;
using IPMS.Domain;
using IPMS.Domain.ValueObjects;

namespace IPMS.Notifications.Engine
{
    public class SuppDockUnDockTimeNotifier : Notifier
    {
        private SuppDryDock _suppDockUnDockTime;
        private ISuppDockUnDockTimeRepository _suppdockundockRepository;

        public SuppDockUnDockTimeNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(SuppDockUnDockTimeNotifier));
                _suppdockundockRepository = new SuppDockUnDockTimeRepository(_NotifierunitOfWork);
                if (pendingNotification != null)
                {
                    _suppDockUnDockTime = _suppdockundockRepository.GetSuppDockUndockDetailsByID(pendingNotification.Reference);
                }
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", _suppDockUnDockTime.CreatedBy));
            }
            else //TODO:Info Email Templates to be defined, hence every email has to send Creator always should go to Agent
            {
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", _suppDockUnDockTime.CreatedBy));
            }

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
            return _userslist;
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _suppDockUnDockTime);
        }

    }
}
