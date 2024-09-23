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
using System.Diagnostics;


namespace IPMS.Notifications.Engine
{
    public class PortEntryPassApplicationNotifier : Notifier
    {
        private PermitRequest _Portentrypass;
        private Stopwatch wfstopwatch;
        protected IPortEntryPassApplicationRepository _portentrypassapplicationrepository;
        
        public PortEntryPassApplicationNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles): base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(PortEntryPassApplicationNotifier));
                _portentrypassapplicationrepository = new PortEntryPassApplicationRepository(_NotifierunitOfWork);
                _Portentrypass = _portentrypassapplicationrepository.GetPortEntryPassdetailsByid(pendingNotification.Reference, pendingNotification.PortCode);
                //_Portentrypass = _NotifierunitOfWork.Repository<PermitRequest>().Query().Select().Where(p => p.PermitRequestID == _Portentrypass.PermitRequestID).Where(apc => apc.PortCode == pendingNotification.PortCode).ToList();

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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "PEP", "", _Portentrypass.PermitRequestID));
            }
            else if (notificationTemplate.NotificationTemplateBase == "M")
            {
               
       
              _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "PEPM", "", _Portentrypass.PermitRequestID));
            }
            else //TODO:Info Email Templates to be defined, hence every email has to send Creator
            {
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "PEP", "", _Portentrypass.PermitRequestID));
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
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.ExternalUser, Roles.Southafricanpoliceservice, _Portentrypass.CreatedBy));

                }
                else if (role.Role.RoleCode == Roles.StateSecurityAgency)
                {
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.ExternalUser, Roles.StateSecurityAgency, _Portentrypass.CreatedBy));
                }
                //else if (role.Role.RoleCode == Roles.PortSecurityOfficer)
                //{
                //    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, Roles.PortSecurityOfficer, _Portentrypass.CreatedBy));
                //}
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
            return Common.GetTokensDictionary(entityDetails, _Portentrypass);
        }
    }
}
