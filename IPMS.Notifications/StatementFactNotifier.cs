using IPMS.Domain.Models;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;


namespace IPMS.Notifications
{
    public class StatementFactNotifier : Notifier
    {
        private StatementFact _statementfactNotification;
        protected IStatementFactRepository _statementfactRepository;

        public StatementFactNotifier(Notification pendingNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(StatementFactNotifier));
                _statementfactRepository = new StatementFactRepository(_NotifierunitOfWork);
                _statementfactNotification = _statementfactRepository.GetStatementFactNotificationByID(Convert.ToString(_statementfactNotification.StatementFactID));
            }
            catch (Exception ex)
            {
                log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            }
        }

        public override string GetPortCode()
        {
            return _statementfactNotification.ArrivalNotification.PortCode;
        }

        public override List<User> GetUsersToBeNotified()
        {
            return _userRepository.GetUsersForRoleAndPortCode(GetPortCode(), this.roles);
        }

        public override Dictionary<string, string> GetTokensDictionary()
        {
            return Common.GetTokensDictionary(entityDetails, _statementfactNotification);
        }
    }
}
