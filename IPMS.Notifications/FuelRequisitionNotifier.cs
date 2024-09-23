using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using Core.Repository;
using IPMS.Data.Context;
using log4net;
using System;
using log4net.Config;

namespace IPMS.Notifications
{
    public class FuelRequisitionNotifier : Notifier
    {
       private FuelRequisition _fuelRequisition;
        protected IFuelRequisitionRepository _fuelRequisitionRepository;

        public FuelRequisitionNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {         
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(FuelRequisitionNotifier));
                _fuelRequisitionRepository = new FuelRequisitionRepository(_NotifierunitOfWork);
                _fuelRequisition = _fuelRequisitionRepository.GetFuelRequisitionRequestDetailsByID(pendingNotification.Reference);
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
            return Common.GetTokensDictionary(entityDetails, _fuelRequisition);
        }
    }
}
