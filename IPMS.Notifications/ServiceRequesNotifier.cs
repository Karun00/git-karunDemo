﻿using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;

namespace IPMS.Notifications
{
    public class ServiceRequestNotifier : Notifier
    {
        private ArrivalNotificationDetails _arrivalNotification;
        private ServiceRequest_VO _serviceRequest;

        protected IServiceRequestRepository _serviceRequestRepository;
        protected IArrivalNotificationRepository _arrivalNotificationRepository;

        public ServiceRequestNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(ServiceRequestNotifier));
                _arrivalNotificationRepository = new ArrivalNotificationRepository(_NotifierunitOfWork);
                _serviceRequestRepository = new ServiceRequestRepository(_NotifierunitOfWork);
                _serviceRequest = _serviceRequestRepository.GetServiceRequestByID(pendingNotification.Reference);
                _arrivalNotification = _arrivalNotificationRepository.GetArrivalNotificationByID(_serviceRequest.VCN);
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

            foreach (var role in this.roles)
            {
                if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                {
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, role.Role.RoleCode, _serviceRequest.ArrivalNotification.AgentID));
                }
                else if (role.Role.RoleCode == Roles.TerminalOperator) //Assumption : UserType.TO and Agent role code('TO') should be same 
                {
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.TerminalOperator, role.Role.RoleCode, Convert.ToInt32(_serviceRequest.ArrivalNotification.TerminalOperatorID)));
                }
                else
                {
                    _roleslist = new List<NotificationRole>();
                    _roleslist.Add(role);
                    _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
                }
            }
            return _userslist;
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _serviceRequest);
        }
    }
}