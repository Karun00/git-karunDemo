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
    public class DepartureNoticeNotifier : Notifier
    {
        private DepartureNoticeVO _DepartureNotice;
        protected IDepartureNoticeRepository _DepartureNoticeRepository;

        public DepartureNoticeNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(DepartureNoticeNotifier));
                _DepartureNoticeRepository = new DepartureNoticeRepository(_NotifierunitOfWork);
                _DepartureNotice = _DepartureNoticeRepository.GetDepartureNoticeByID(pendingNotification.Reference);
            }
            catch (Exception ex)
            {
                log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            }
        }

        public override string GetPortCode()
        {
            return _DepartureNotice.PortCode;
        }

        public override List<User> GetUsersToBeNotified()
        {
            List<User> _userslist = new List<User>();
            List<NotificationRole> _roleslist = new List<NotificationRole>();

            if (notificationTemplate.NotificationTemplateBase == "U")
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), pendingNotification.UserType, "", pendingNotification.UserTypeId));
            else
            {
                foreach (var role in this.roles)
                {
                    if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                    {
                        _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Agent, role.Role.RoleCode, _DepartureNotice.AgentID));
                    }
                    else if (role.Role.RoleCode == Roles.TerminalOperator) //Assumption : UserType.TO and Agent role code('TO') should be same 
                    {
                        //Not required
                    }
                    else
                    {
                        _roleslist = new List<NotificationRole>();
                        _roleslist.Add(role);
                        _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), UserType.Employee, role.Role.RoleCode, 0));
                    }
                }
            }
            return _userslist;
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _DepartureNotice);
        }
    }
}