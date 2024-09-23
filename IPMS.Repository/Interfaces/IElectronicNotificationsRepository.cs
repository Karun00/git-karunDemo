using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public interface IElectronicNotificationsRepository
    {
        List<NotificationDetails> GetNotificationTemplates(string portCode);
        List<Role> GetNotificationRoles(string notificationTemplateCode, string portCode);
        List<Port> GetNotificationPorts(string notificationTemplateCode);
    }
}
