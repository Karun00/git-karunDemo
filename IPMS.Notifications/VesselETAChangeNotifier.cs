﻿using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using log4net;
using System.Collections.Generic;
using System;
using log4net.Config;

namespace IPMS.Notifications
{
    public class VesselETAChangeNotifier : Notifier
    {
        private VesselETAChange _vesseletachange;
        protected IVesselETAChangeRepository _vesseletachangeRepository;

        public VesselETAChangeNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(VesselETAChangeNotifier));
                _vesseletachangeRepository = new VesselETAChangeRepository(_NotifierunitOfWork);
                _vesseletachange = _vesseletachangeRepository.GetVesselETAChangeDetailsByID(pendingNotification.Reference);
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
            return _userRepository.GetUsersForRoleAndPortCode(GetPortCode(), this.roles);
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _vesseletachange);
        }

    }
}