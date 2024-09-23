using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using IPMS.Repository;
using log4net;
using log4net.Config;
using IPMS.Domain;
using System.Linq;
using IPMS.Domain.ValueObjects;

namespace IPMS.Notifications.Engine
{
    public class BerthMaintenanceCompletionNotifier : Notifier
    {
        private BerthMaintenanceDataVO _berthMaintenanceCompletion;
        private IBerthMaintenanceCompletionRepository _berthMaintenanceCompletionRepository;

        public BerthMaintenanceCompletionNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            //try
            //{
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(BerthMaintenanceCompletionNotifier));
                _berthMaintenanceCompletionRepository = new BerthMaintenanceCompletionRepository(_NotifierunitOfWork);
                if (pendingNotification != null)
                {
                    _berthMaintenanceCompletion = _berthMaintenanceCompletionRepository.GetBerthMaintenanceCompletionDetailsByID(pendingNotification.Reference);
                }
        //}
        //    catch (Exception ex)
        //    {
        //        log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
        //    }
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), pendingNotification.UserType, "", _berthMaintenanceCompletion.CreatedBy));
            else
            {
                foreach (var role in this.roles)
                {
                    if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                    {
                        //Not Required
                    }
                    else if (role.Role.RoleCode == Roles.TerminalOperator) //Assumption : UserType.TO and Agent role code('TO') should be same 
                    {
                        //Not required
                    }
                    else if (role.Role.RoleCode == Roles.Southafricanpoliceservice)
                    {
                        //Not Required
                    }
                    else if (role.Role.RoleCode == Roles.StateSecurityAgency)
                    {
                        //Not Required
                    }
                    else
                    {
                        _roleslist = new List<NotificationRole>();
                        _roleslist.Add(role);
                        _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), pendingNotification.UserType, role.Role.RoleCode, 0));
                    }
                }
            }
            return _userslist.GroupBy(x => x.UserID).Select(x => x.First()).ToList();
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _berthMaintenanceCompletion);
        }
    }
}
