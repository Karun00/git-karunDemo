using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using Core.Repository;
using IPMS.Data.Context;
using log4net;
using System;
using log4net.Config;
using IPMS.Domain.ValueObjects;

namespace IPMS.Notifications.Engine
{
   public class BerthOccupationNotifier:Notifier
    {
       //private BerthOccupation _berthoccupation;
       // protected IBerthOccupationRepository _berthoccupationRepository;

       public BerthOccupationNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {         
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(BerthOccupationNotifier));
                //_berthoccupationRepository = new BerthOccupationRepository(_NotifierunitOfWork);
                //_berthoccupation = _berthoccupationRepository.GeBerthOccupationRequestDetailsByID(pendingNotification.Reference);
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
           // return Common.GetTokensDictionary(entityDetails, _berthoccupation);
            return null;
        }
    }
}
