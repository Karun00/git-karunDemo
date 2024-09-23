using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using System.ComponentModel;
using IPMS.Domain.ValueObjects;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using log4net;
using Core.Repository;
using IPMS.Data.Context;
using log4net.Config;
using IPMS.Domain;

namespace IPMS.Notifications
{
    class AgentRegistrationNotifier : Notifier
    {
        private Agent _agentRegistration;

        public AgentRegistrationNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(AgentRegistrationNotifier));
                _agentRegistration = _NotifierunitOfWork.Repository<Agent>().Query().Select().Where(t => t.AgentID == Convert.ToInt32(pendingNotification.Reference)).FirstOrDefault<Agent>();
                _agentRegistration.AgentPorts = _NotifierunitOfWork.Repository<AgentPort>().Query().Select().Where(ap => ap.AgentID == _agentRegistration.AgentID).Where(apc => apc.PortCode == pendingNotification.PortCode).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Exception = " + ex.Message);
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), pendingNotification.UserType, "", pendingNotification.UserTypeId));
            else
            {
                foreach (var role in this.roles)
                {
                    if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                    {
                        _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, role.Role.RoleCode, _agentRegistration.AgentID));
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
            }
            return _userslist;


        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _agentRegistration);
        }
    }
}
