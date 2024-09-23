using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using log4net;
using System.Collections.Generic;
using System;
using System.Globalization;
using log4net.Config;
using IPMS.Domain;
using System.Linq;
using IPMS.Domain.ValueObjects;

namespace IPMS.Notifications.Engine
{
    public class VesselCallAnchorageNotifier : Notifier
    {
        private VesselCallAnchorageVO vesselcallanchorage;
        protected IVesselCallRepository VesselcallanchorageRepository;
        private int _agentid;
        private int _terminalOperatorId = 0;
        public VesselCallAnchorageNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(VesselCallAnchorageNotifier));
                VesselcallanchorageRepository = new VesselCallRepository(_NotifierunitOfWork);
                if (pendingNotification != null)
                {
                    if (pendingNotification.NotificationTemplateCode == WFStatus.CaptureArrival)
                    {
                        vesselcallanchorage = VesselcallanchorageRepository.GetVesselCallDetailsById(pendingNotification.Reference);
                        if (vesselcallanchorage != null)
                        {
                            var agentdetails = _NotifierunitOfWork.Repository<User>().Queryable().FirstOrDefault(e => e.UserID == vesselcallanchorage.CreatedBy && e.UserType == UserType.Agent);
                            _agentid = agentdetails != null ? agentdetails.UserTypeID : 0;
                            var terminal = _NotifierunitOfWork.Repository<ArrivalNotification>().Queryable().FirstOrDefault(a => a.VCN == vesselcallanchorage.VCN);
                            _terminalOperatorId = terminal != null ? Convert.ToInt32(terminal.TerminalOperatorID, CultureInfo.InvariantCulture) : 0;
                        }
                    }

                    else
                    {
                        vesselcallanchorage = VesselcallanchorageRepository.GetVesselCallAnchorageDetailsById(pendingNotification.Reference);
                        if (vesselcallanchorage != null)
                        {
                            var agentdetails = _NotifierunitOfWork.Repository<User>().Queryable().FirstOrDefault(e => e.UserID == vesselcallanchorage.CreatedBy && e.UserType == UserType.Agent);
                            _agentid = agentdetails != null ? agentdetails.UserTypeID : 0;
                            var terminal = _NotifierunitOfWork.Repository<ArrivalNotification>().Queryable().FirstOrDefault(a => a.VCN == vesselcallanchorage.VCN);
                            _terminalOperatorId = terminal != null ? Convert.ToInt32(terminal.TerminalOperatorID, CultureInfo.InvariantCulture) : 0;
                        }
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
            List<User> userslist = new List<User>();
            List<NotificationRole> roleslist = new List<NotificationRole>();

            if (notificationTemplate.NotificationTemplateBase == "U")
            {
                userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", vesselcallanchorage.CreatedBy));
            }
            else //TODO:Info Email Templates to be defined, hence every email has to send Creator
            {
                userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", vesselcallanchorage.CreatedBy));
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
                    userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.TerminalOperator, "", _terminalOperatorId));

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
            return Common.GetTokensDictionary(entityDetails, vesselcallanchorage);
        }
    }
}

