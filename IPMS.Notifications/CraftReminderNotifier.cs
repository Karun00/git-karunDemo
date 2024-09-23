using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Services.WorkFlow;
using log4net;
using System;
using System.Collections.Generic;
using IPMS.Services;
using IPMS.Repository;

namespace IPMS.Notifications
{
    public class CraftReminderNotifier : Notifier
    {
        private CraftReminderConfigVO _craftreminderconfig;
        protected ICraftReminderConfigRepository _craftreminderconfigRepository;
        public CraftReminderNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                log = LogManager.GetLogger(typeof(UserRegistrationNotifier));
                _craftreminderconfig = _craftreminderconfigRepository.GetCraftReminderConfigByConfigID(Convert.ToInt32(pendingNotification.Reference));
                
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
            return Common.GetTokensDictionary(entityDetails, _craftreminderconfig);
        }

    }
}
