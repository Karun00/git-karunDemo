using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using log4net;
using System.Collections.Generic;
using System;
using log4net.Config;
using IPMS.Domain;
using System.Linq;
using IPMS.Domain.ValueObjects;

namespace IPMS.Notifications.Engine
{
    public class BerthPreSchedulingNotifier : Notifier
    {
        private VesselCallMovement _vesselcallmoment;
        protected IBerthPreSchedulingRepository _berthpreschedulingRepository;
        private int _agentid = 0;

        public BerthPreSchedulingNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(BerthPreSchedulingNotifier));
                _berthpreschedulingRepository = new BerthPreSchedulingRepository(_NotifierunitOfWork);
                _vesselcallmoment = _berthpreschedulingRepository.GetVesselCallMomentDetailsById(pendingNotification.Reference);
                _agentid = _NotifierunitOfWork.Repository<VesselCall>().Queryable().Where(vc => vc.VCN == _vesselcallmoment.VCN).FirstOrDefault<VesselCall>().RecentAgentID;
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", _vesselcallmoment.CreatedBy));
            else
            {
                foreach (var role in this.roles)
                {
                    if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                    {
                        _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, "", _agentid));
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
            Dictionary<string, string> messageTemplatePlaceHolders = Common.GetTokensDictionary(entityDetails, _vesselcallmoment);

            try
            {
                if (string.IsNullOrEmpty(messageTemplatePlaceHolders["%FromPositionPortCode%"]))
                {
                    if (!string.IsNullOrEmpty(_vesselcallmoment.FromPositionPortCode.ToString()))
                    {
                        messageTemplatePlaceHolders["%FromPositionPortCode%"] = _vesselcallmoment.FromPositionPortCode.ToString();
                    }

                }

            }
            catch (KeyNotFoundException)
            {
                //Nothing to do 
            }

            return messageTemplatePlaceHolders;
        }

    }
}
