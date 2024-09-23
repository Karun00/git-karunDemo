using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface ISystemNotificationRepository
    {
        List<SystemNotification> GetNotifications(string portCode, int userID);

        List<SystemNotification> GetAllNotifications(string portCode, int userID);
    }
}
