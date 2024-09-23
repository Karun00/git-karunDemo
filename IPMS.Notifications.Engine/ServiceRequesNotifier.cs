using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using log4net.Util;

namespace IPMS.Notifications.Engine
{
    public class ServiceRequestNotifier : Notifier
    {
        private ArrivalNotificationDetails _arrivalNotification;
        private ServiceRequest_VO _serviceRequest;
        private VesselCall _vesselcall;

        protected IServiceRequestRepository _serviceRequestRepository;
        protected IArrivalNotificationRepository _arrivalNotificationRepository;
        protected IVesselCallRepository _vesselcallRepository;

        public ServiceRequestNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(ServiceRequestNotifier));
                _arrivalNotificationRepository = new ArrivalNotificationRepository(_NotifierunitOfWork);
                _serviceRequestRepository = new ServiceRequestRepository(_NotifierunitOfWork);
                _vesselcallRepository = new VesselCallRepository(_NotifierunitOfWork);

                Stopwatch wfstopwatch;
                wfstopwatch = Stopwatch.StartNew();
                log.Debug("GetServiceRequestByID methods Started..");
                _serviceRequest = _serviceRequestRepository.GetServiceRequestByID(pendingNotification.Reference);
                wfstopwatch.Stop();
                log.Debug("GetServiceRequestByID methods Completed.." + wfstopwatch.Elapsed.ToString());

                _arrivalNotification = _arrivalNotificationRepository.GetArrivalNotificationById(_serviceRequest.VCN);
                _vesselcall = _vesselcallRepository.VesselCallDetails(_serviceRequest.VCN);
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", _serviceRequest.CreatedBy));
            }
            else //TODO:Info Email Templates to be defined, hence every email has to send Creator always should go to Agent
            {
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", _serviceRequest.CreatedBy));
            }

            foreach (var role in this.roles)
            {
                if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                {
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, "", _vesselcall.RecentAgentID));
                }
                else if (role.Role.RoleCode == Roles.TerminalOperator) //Assumption : UserType.TO and Agent role code('TO') should be same 
                {
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.TerminalOperator, role.Role.RoleCode, Convert.ToInt32(_serviceRequest.ArrivalNotification.TerminalOperatorID, CultureInfo.InvariantCulture)));
                }
                else
                {
                    _roleslist = new List<NotificationRole>();
                    _roleslist.Add(role);
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
                }
            }
            //TODO:Need to be implement GroupBy in all Notifiers where ever not implemented, otherwise same user can have multiple times in differant roles
            return _userslist.GroupBy(x => x.UserID).Select(x => x.First()).ToList();
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            Dictionary<string, string> messageTemplatePlaceHolders = Common.GetTokensDictionary(entityDetails, _serviceRequest);

            try
            {
                if (string.IsNullOrEmpty(messageTemplatePlaceHolders["%PortCode%"]))
                {
                    messageTemplatePlaceHolders["%PortCode%"] = _arrivalNotification.PortCode;
                }

            }
            catch (KeyNotFoundException)
            {
                //Nothing to do 
            }

            try
            {
                if (string.IsNullOrEmpty(messageTemplatePlaceHolders["%PortName%"]))
                {
                    var PortName = _portRepository.GetPortDetailsByPortCode(pendingNotification.PortCode).PortName;
                    messageTemplatePlaceHolders["%PortName%"] = PortName;
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
