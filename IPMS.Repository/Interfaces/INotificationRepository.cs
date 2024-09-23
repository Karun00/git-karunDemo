using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public interface INotificationRepository
    {
        List<Notification> GetPendingNotifications(int laterThanId);
        List<NotificationTemplate> GetNotificationTemplateByEntityTask(int entityId, string workFlowTaskCode, string portcode);
        List<NotificationTemplate> GetNotificationTemplateByEntityTask(int entityId, string workFlowTaskCode, string portcode, string NotificationTemplateBase, int PermitRequestID, string EmailAddress);
        bool PushMessageToQueue(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode, string NotificationTemplateCode);
        List<NotificationVO> GetAuraNotConfirmedAutoEmailInfo();
        List<NotificationVO> GetServiceRequestAutoEmailRule1();
        List<NotificationVO> GetServiceRequestAutoEmailRule2();
        List<NotificationVO> GetServiceRequestAutoEmailRule3();
        List<NotificationVO> GetCraftReminderAutoEmailDaily();
        List<NotificationVO> GetCraftReminderAutoEmailWeekly();
        List<NotificationVO> GetCraftReminderAutoEmailMonthly();
        
    }
}
