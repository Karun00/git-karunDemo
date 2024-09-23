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
    public class VesselETAChangeNotifier : Notifier
    {
        private VesselETAChangeVO vesseletachange;
        protected IVesselETAChangeRepository VesseletachangeRepository;
        private int _agentid;
        public VesselETAChangeNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(VesselETAChangeNotifier));
                VesseletachangeRepository = new VesselETAChangeRepository(_NotifierunitOfWork);
                if (pendingNotification != null)
                {
                    vesseletachange = VesseletachangeRepository.GetVesselEtaChangeDetailsById(pendingNotification.Reference);
                    //Assumption : Change ETA Done by only AGENT no other users cann't raise
                    _agentid = _NotifierunitOfWork.Repository<User>().Queryable().Where(e => e.UserID == vesseletachange.CreatedBy && e.UserType == UserType.Agent).FirstOrDefault<User>().UserTypeID;
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
            List<User> userslist = new List<User>();
            List<NotificationRole> roleslist = new List<NotificationRole>();

            if (notificationTemplate.NotificationTemplateBase == "U")
            {
                userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", vesseletachange.CreatedBy));
            }
            else //TODO:Info Email Templates to be defined, hence every email has to send Creator
            {
                userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", vesseletachange.CreatedBy));
            }

            foreach (var role in this.roles)
            {
                if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                {
                    userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, role.Role.RoleCode, _agentid));
                }
                else if (role.Role.RoleCode == Roles.BerthPlanner) //Assumption : UserType.TO and Agent role code('TO') should be same 
                {
                    userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
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
                    roleslist = new List<NotificationRole>();
                    roleslist.Add(role);
                    userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
                }
            }

            return userslist.GroupBy(x => x.UserID).Select(x => x.First()).ToList();
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, vesseletachange);
        }
    }
}
