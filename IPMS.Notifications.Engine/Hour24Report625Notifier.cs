using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using IPMS.Data.Context;
using System.Linq;
using System.Globalization;
using IPMS.Domain.ValueObjects;

namespace IPMS.Notifications.Engine
{
    public class Hour24Report625Notifier : Notifier
    {

        private Hour24Report625 _hours24report625;
        private IHour24AndSection625Repository _hours24report625Repository;

        public Hour24Report625Notifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {

            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(PortEntryPassApplicationNotifier));
                _hours24report625Repository = new Hour24AndSection625Repository(_NotifierunitOfWork);
                _hours24report625 = _hours24report625Repository.Get24HoursreportDetailsForNotification(pendingNotification.PortCode, Convert.ToInt32(pendingNotification.Reference,CultureInfo.InvariantCulture));
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "HSR", "", _hours24report625.Hour24Report625ID));
            }
            else //TODO:Info Email Templates to be defined, hence every email has to send Creator
            {
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, Roles.VesselTrafficController, _hours24report625.Hour24Report625ID));
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
                //else if (role.Role.RoleCode == Roles.VesselTrafficController)
                //{
                //    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, Roles.VesselTrafficController, _hours24report625.CreatedBy));
                //}        
                else
                {
                    _roleslist = new List<NotificationRole>();
                    _roleslist.Add(role);
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
                }
            }
            return _userslist.GroupBy(x => x.UserID).Select(x => x.First()).ToList();

            //return _userRepository.GetUsersForRoleAndPortCodeByUserType(GetPortCode(), this.roles, pendingNotification.UserType, pendingNotification.UserTypeId);
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _hours24report625);
        }
    }
}
