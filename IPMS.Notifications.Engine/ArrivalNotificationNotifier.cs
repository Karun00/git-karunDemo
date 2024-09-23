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
using log4net.Config;
using IPMS.Domain;
using System.Globalization;

namespace IPMS.Notifications.Engine
{
    public class ArrivalNotificationNotifier : Notifier
    {
        private ArrivalNotificationDetails _arrivalNotification;
        protected IArrivalNotificationRepository _arrivalNotificationRepository;
        private int _agentid =0;
        private int _terminalOperatorId = 0;

        public ArrivalNotificationNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(ArrivalNotificationNotifier));
                _arrivalNotificationRepository = new ArrivalNotificationRepository(_NotifierunitOfWork);
                _arrivalNotification = _arrivalNotificationRepository.GetArrivalNotificationById(pendingNotification.Reference);
                _agentid = _NotifierunitOfWork.Repository<ArrivalNotification>().Queryable().Where(a => a.VCN == _arrivalNotification.VCN).FirstOrDefault<ArrivalNotification>().AgentID;
                _terminalOperatorId = Convert.ToInt32(_NotifierunitOfWork.Repository<ArrivalNotification>().Queryable().Where(a => a.VCN == _arrivalNotification.VCN).FirstOrDefault<ArrivalNotification>().TerminalOperatorID, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            }
        }

        public override string GetPortCode()
        {
            return _arrivalNotification.PortCode;
        }

        public override List<User> GetUsersToBeNotified()
        {
            List<User> _userslist = new List<User>();
            List<NotificationRole> _roleslist = new List<NotificationRole>();

            if (notificationTemplate.NotificationTemplateBase == "U")
            {
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, "", _agentid));
            }
            else //TODO:Info Email Templates to be defined, hence every email has to send Creator
            {
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, "", _agentid));
            }

            foreach (var role in this.roles)
            {
                if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                {
                    //ALREADY ADDED AGENT IN ABOVE CONDITION
                }
                else if (role.Role.RoleCode == Roles.TerminalOperator && _terminalOperatorId > 0) //Assumption : UserType.TO and Agent role code('TO') should be same 
                {
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.TerminalOperator, "", _terminalOperatorId));
                }
                else
                {
                    _roleslist = new List<NotificationRole>();
                    _roleslist.Add(role);
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
                }
            }
            return _userslist.GroupBy(x => x.UserID).Select(x => x.First()).ToList();

            //return _userRepository.GetUsersForRoleAndPortCode(GetPortCode(), this.roles);
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _arrivalNotification);
        }
    }
}
