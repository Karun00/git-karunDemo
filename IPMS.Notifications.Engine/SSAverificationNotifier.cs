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

namespace IPMS.Notifications.Engine
{
    public class SSAverificationNotifier : Notifier
      {
        private PermitRequest _SSAverification;
        protected IPortEntryPassApplicationRepository _portentrypassapplicationrepository;

        public SSAverificationNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(SSAverificationNotifier));
                _portentrypassapplicationrepository = new PortEntryPassApplicationRepository(_NotifierunitOfWork);
                _SSAverification = _portentrypassapplicationrepository.GetPortEntryPassdetailsByid(pendingNotification.Reference, pendingNotification.PortCode);
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "PEP", "", _SSAverification.CreatedBy));
            }
            else //TODO:Info Email Templates to be defined, hence every email has to send Creator
            {
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "PEP", "", _SSAverification.CreatedBy));
            }

            foreach (var role in this.roles)
            {
                if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                {
                    //AGENT ID NOT FOUND IN THIS REQUEST
                }
                else if (role.Role.RoleCode == Roles.TerminalOperator) //Assumption : UserType.TO and Agent role code('TO') should be same 
                {
                    //TERMINALOPERATOR ID NOT FOUND IN THIS REQUEST
                }
                else if (role.Role.RoleCode == Roles.Southafricanpoliceservice)
                {
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.ExternalUser, Roles.Southafricanpoliceservice, _SSAverification.CreatedBy));

                }
                else if (role.Role.RoleCode == Roles.StateSecurityAgency)
                {
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.ExternalUser, Roles.StateSecurityAgency, _SSAverification.CreatedBy));
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
            return Common.GetTokensDictionary(entityDetails, _SSAverification);
        }
    


    
    }
}
