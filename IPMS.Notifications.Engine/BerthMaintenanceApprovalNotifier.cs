using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using Core.Repository;
using IPMS.Data.Context;
using log4net;
using System;
using log4net.Config;
using IPMS.Domain;
using System.Linq;
using IPMS.Domain.ValueObjects;

namespace IPMS.Notifications.Engine
{
    public class BerthMaintenanceApprovalNotifier : Notifier
    {
        private BerthMaintenanceVO _berthMaintenance;
        private IBerthMaintenanceRepository _berthMaintenanceRepository;

        public BerthMaintenanceApprovalNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            //try
            //{
            //    XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(BerthMaintenanceApprovalNotifier));
                _berthMaintenanceRepository = new BerthMaintenanceRepository(_NotifierunitOfWork);
                if (pendingNotification != null)
                {
                    _berthMaintenance = _berthMaintenanceRepository.GetBerthMaintenanceRequestDetailsByID(pendingNotification.Reference);
                }
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            //}
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, "", _berthMaintenance.CreatedBy));
            }
            foreach (var role in this.roles)
            {
                if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                {
                    //Agent ID will not be available
                }
                else if (role.Role.RoleCode == Roles.TerminalOperator) //Assumption : UserType.TO and Agent role code('TO') should be same 
                {
                    //Terminal Operator ID will not available for this request
                }
                else
                {
                    _roleslist = new List<NotificationRole>();
                    _roleslist.Add(role);
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
                }
            }
            return _userslist.GroupBy(x => x.UserID).Select(x => x.First()).ToList();
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _berthMaintenance);
        }
    }
}
