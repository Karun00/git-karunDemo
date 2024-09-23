using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain;
using System.Globalization;

namespace IPMS.Repository
{
    public class SystemNotificationRepository : ISystemNotificationRepository
    {
        private IUnitOfWork _unitOfWork;
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;

        public SystemNotificationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
        }

        public List<SystemNotification> GetNotifications(string portCode, int userID)
        {

            var notificationLifeSpan = _portGeneralConfigurationRepository.GetPortConfiguration(portCode, ConfigName.NotificationLifeSpan);

            double showForLastNHours = Convert.ToDouble(notificationLifeSpan, CultureInfo.InvariantCulture) * -1;
            DateTime showUntilTime = DateTime.Now.AddHours(showForLastNHours);
            var newNotifications = _unitOfWork.Repository<SystemNotification>().Queryable().Where(x => x.UserID == userID && x.PortCode == portCode && x.IsRead == UserLogin.LoggedIn && x.CreatedDate >= showUntilTime).OrderByDescending(x => x.NotificationId).ToList();

            return newNotifications;
        }

        public List<SystemNotification> GetAllNotifications(string portCode, int userID)
        {

            var notificationLifeSpan = _portGeneralConfigurationRepository.GetPortConfiguration(portCode, ConfigName.NotificationLifeSpan);


            var notifications = (from sn in _unitOfWork.Repository<SystemNotification>().Query().Select()
                                 where sn.UserID == userID && sn.PortCode == portCode && sn.CreatedDate.AddHours(Convert.ToDouble(notificationLifeSpan, CultureInfo.InvariantCulture)) >= DateTime.Now
                                 orderby sn.NotificationId descending
                                 select sn).ToList();

            return notifications;
        }
    }
}
