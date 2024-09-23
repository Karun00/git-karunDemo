using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Services.WorkFlow;
using log4net;
using System;
using System.Collections.Generic;
using IPMS.Services;
using IPMS.Domain;
using System.Linq;
using System.Text;
using System.Globalization;


namespace IPMS.Notifications.Engine
{
    public class UserNotifier : Notifier
    {
        private UserMasterVO _user;

        public UserNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                log = LogManager.GetLogger(typeof(UserRegistrationNotifier));
                _user = _userRepository.GetUserByUserID(Convert.ToInt32(pendingNotification.Reference, CultureInfo.InvariantCulture));
                _user.PWD = Password.Decrypt(_user.PWD, true);
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
                _userslist.AddRange(_userRepository.GetUserDetailsForRoleAndPortCodeByUserType(GetPortCode(), "", "", Convert.ToInt32(pendingNotification.Reference)));
            }
            return _userslist.GroupBy(x => x.UserID).Select(x => x.First()).ToList();
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _user);
        }

    }
}
