using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IElectronicNotificationsService : IDisposable
    {
        [OperationContract]
        string AddNotification(NotificationTemplate notificationdata);

        [OperationContract]
        List<Entity> GetEntityDetails();

        [OperationContract]
        List<string> GetTokens(int id);

        [OperationContract]
        List<Role> GetRolesDetails();

        [OperationContract]
        List<Port> GetPortsDetails();

        [OperationContract]
        List<NotificationDetails> GetNotifications();

        [OperationContract]
        string DeleteNotification(NotificationTemplate notificationdata);

        [OperationContract]
        string ModifyNotification(NotificationTemplate notificationdata);

        [OperationContract]
        List<SubCategoryVO> GetWorkflowStatus();

        //[OperationContract]
        //string AddNotificationAsync(NotificationTemplate notificationdata);

        //[OperationContract]
        //List<Entity> GetEntityDetailsAsync();

        //[OperationContract]
        //List<string> GetTokensAsync(int id);

        //[OperationContract]
        //List<Role> GetRolesDetailsAsync();

        //[OperationContract]
        //List<Port> GetPortsDetailsAsync();

        //[OperationContract]
        //List<NotificationDetails> GetNotificationsAsync();

        //[OperationContract]
        //string DeleteNotificationAsync(NotificationTemplate notificationdata);

        //[OperationContract]
        //string ModifyNotificationAsync(NotificationTemplate notificationdata);

        //[OperationContract]
        //List<SubCategoryVO> GetWorkflowStatusAsync();

        [OperationContract]
        List<NotificationTemplateVO> GetAllNotifications();

        //[OperationContract]
        //Task<List<NotificationTemplateVO>> GetAllNotificationsAsync();

    }
}