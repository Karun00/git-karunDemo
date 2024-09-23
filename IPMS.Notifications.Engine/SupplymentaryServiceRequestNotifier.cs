using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using log4net;
using System;
using log4net.Config;
using IPMS.Domain;
using System.Globalization;

namespace IPMS.Notifications.Engine
{
    public class SupplymentaryServiceRequestNotifier : Notifier
    {
        private SuppServiceRequestVO _supplymentaryServiceRequest;
        private ISupplymentaryServiceRepository _supplymentaryServiceRequestRepositoy;
        private int? _agentId;
        private int agentId;

        public SupplymentaryServiceRequestNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(SupplymentaryServiceRequestNotifier));
                _supplymentaryServiceRequestRepositoy = new SupplymentaryServiceRepository(_NotifierunitOfWork);
                _supplymentaryServiceRequest = _supplymentaryServiceRequestRepositoy.GetSuppServiceRequestByID(Convert.ToInt32(pendingNotification.Reference, CultureInfo.InvariantCulture));
                _agentId = _supplymentaryServiceRequest.AgentId;
                agentId = ((int)(_agentId == null ? 0 : _agentId));
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, "", agentId));
            }
            else
            {
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, "", agentId));
            }
            foreach (var role in this.roles)
            {
                if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                {
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, role.Role.RoleCode, agentId));
                    //AGENT ID NOT FOUND IN THIS REQUEST
                }
                else if (role.Role.RoleCode == Roles.TerminalOperator) //Assumption : UserType.TO and Agent role code('TO') should be same 
                {
                    //TERMINALOPERATOR ID NOT FOUND IN THIS REQUEST
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

            //else //TODO:Info Email Templates to be defined, hence every email has to send Creator
            //{
            //    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", _supplymentaryServiceRequest.ModifiedBy));
            //}

            //foreach (var role in this.roles)
            //{
            //    if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
            //    {
            //        //NOT REQUIRED
            //    }
            //    else if (role.Role.RoleCode == Roles.TerminalOperator) //Assumption : UserType.TO and Agent role code('TO') should be same 
            //    {
            //        //NOT REQUIRED
            //    }
            //    else
            //    {
            //        _roleslist = new List<NotificationRole>();
            //        _roleslist.Add(role);
            //        _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
            //    }
            //}
            return _userslist;
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _supplymentaryServiceRequest);
        }
    }
}
