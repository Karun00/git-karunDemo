﻿using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;

namespace IPMS.Notifications
{
    public class LicensingRequestNotifier : Notifier
    {
        private LicenseRequest _licenserwequest;
        protected ILicensingRequestRepository _licenseRequestRepository;

        public LicensingRequestNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {

            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(LicensingRequestNotifier));
                _licenseRequestRepository = new LicensingRequestRepository(_NotifierunitOfWork);
                _licenserwequest = _licenseRequestRepository.GetLicensingRequestDetailsByID(Convert.ToInt32(pendingNotification.Reference));
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "LR", "", _licenserwequest.LicenseRequestID));
            }
          

                foreach (var role in this.roles)
                {
                    if (role.Role.RoleCode == Roles.Agent) //Assumption : UserType.Agent and Agent role code('AGNT') should be same 
                    {
                        //Not required
                    }
                    else if (role.Role.RoleCode == Roles.TerminalOperator) //Assumption : UserType.TO and Agent role code('TO') should be same 
                    {
                        //not required
                    }
                    else
                    {
                        _roleslist = new List<NotificationRole>();
                        _roleslist.Add(role);
                        _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), pendingNotification.UserType, role.Role.RoleCode, 0));
                    }
                }
            return _userslist;

            //return _userRepository.GetUsersForRoleAndPortCodeByUserType(GetPortCode(), this.roles, pendingNotification.UserType, pendingNotification.UserTypeId);
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _licenserwequest);
        }
    }
}

