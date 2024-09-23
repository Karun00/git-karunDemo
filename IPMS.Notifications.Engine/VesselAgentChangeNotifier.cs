using IPMS.Domain.Models;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using log4net;
using log4net.Config;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using IPMS.Domain;
using IPMS.Domain.ValueObjects;

namespace IPMS.Notifications.Engine
{
    public class VesselAgentChangeNotifier : Notifier
    {
        private VesselAgentChange vesselAgentChangeNotification;
        protected IVesselAgentChangeRepository vesselAgentChangeRepository;

        public VesselAgentChangeNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(VesselAgentChangeNotifier));
                vesselAgentChangeRepository = new VesselAgentChangeRepository(_NotifierunitOfWork);
                vesselAgentChangeNotification = vesselAgentChangeRepository.GetVesselAgentcahngeNotificationById(pendingNotification.Reference);
                if (vesselAgentChangeNotification != null)
                {
                    if (vesselAgentChangeNotification.ArrivalNotification != null)
                    {
                        vesselAgentChangeNotification.ArrivalNotification =_NotifierunitOfWork.Repository<ArrivalNotification>().Queryable().Where(an => an.VCN == vesselAgentChangeNotification.VCN).FirstOrDefault<ArrivalNotification>();
                        if (vesselAgentChangeNotification.ArrivalNotification.Vessel != null)
                            vesselAgentChangeNotification.ArrivalNotification.Vessel =_NotifierunitOfWork.Repository<Vessel>().Queryable().Where(v => v.VesselID == vesselAgentChangeNotification.ArrivalNotification.VesselID).FirstOrDefault<Vessel>();
                    }
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

            //Adding User for Propsed Agent
            if (vesselAgentChangeNotification.ProposedAgent > 0)
            {
                User _ProposedAgent = _userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, "", vesselAgentChangeNotification.ProposedAgent).FirstOrDefault<User>();
                _userslist.Add(_ProposedAgent);
            }
            //Adding User for CurrentAgent
            if (vesselAgentChangeNotification.CurrentAgentID > 0)
            {
                User _CurrentAgent = _userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, "", Convert.ToInt32(vesselAgentChangeNotification.CurrentAgentID, CultureInfo.InvariantCulture)).FirstOrDefault<User>();
                _userslist.Add(_CurrentAgent);
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
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
                }
            }

            return _userslist.GroupBy(x => x.UserID).Select(x => x.First()).ToList();
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, vesselAgentChangeNotification);
        }

    }
}
