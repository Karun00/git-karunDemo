using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;


namespace IPMS.ServiceProxies.Clients
{
    public class ElectronicNotificationsClient : UserClientBase<IElectronicNotificationsService>, IElectronicNotificationsService
    {
        public string AddNotification(NotificationTemplate notificationdata)
        {
            return WrapOperationWithException(() => Channel.AddNotification(notificationdata));
        }

        public List<Entity> GetEntityDetails()
        {
            return WrapOperationWithException(() => Channel.GetEntityDetails());
        }

        public List<string> GetTokens(int id)
        {
            return WrapOperationWithException(() => Channel.GetTokens(id));
        }

        public List<Role> GetRolesDetails()
        {
            return WrapOperationWithException(() => Channel.GetRolesDetails());
        }

        public List<Port> GetPortsDetails()
        {
            return WrapOperationWithException(() => Channel.GetPortsDetails());
        }

        public List<NotificationDetails> GetNotifications()
        {
            return WrapOperationWithException(() => Channel.GetNotifications());
        }

        public string DeleteNotification(NotificationTemplate notificationdata)
        {
            return WrapOperationWithException(() => Channel.DeleteNotification(notificationdata));
        }

        public string ModifyNotification(NotificationTemplate notificationdata)
        {
            return WrapOperationWithException(() => Channel.ModifyNotification(notificationdata));
        }

        public List<SubCategoryVO> GetWorkflowStatus()
        {
            return WrapOperationWithException(() => Channel.GetWorkflowStatus());
        }

        //public string AddNotificationAsync(NotificationTemplate notificationdata)
        //{
        //    return WrapOperationWithException(() => Channel.AddNotificationAsync(notificationdata));
        //}

        //public List<Entity> GetEntityDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetEntityDetailsAsync());
        //}

        //public List<string> GetTokensAsync(int id)
        //{
        //    return WrapOperationWithException(() => Channel.GetTokensAsync(id));
        //}

        //public List<Role> GetRolesDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetRolesDetailsAsync());
        //}

        //public List<Port> GetPortsDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetPortsDetailsAsync());
        //}

        //public List<NotificationDetails> GetNotificationsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetNotificationsAsync());
        //}

        //public string DeleteNotificationAsync(NotificationTemplate notificationdata)
        //{
        //    return WrapOperationWithException(() => Channel.DeleteNotificationAsync(notificationdata));
        //}

        //public string ModifyNotificationAsync(NotificationTemplate notificationdata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyNotificationAsync(notificationdata));
        //}

        //public List<SubCategoryVO> GetWorkflowStatusAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetWorkflowStatusAsync());
        //}
        public List<NotificationTemplateVO> GetAllNotifications()
        {
            return WrapOperationWithException(() => Channel.GetAllNotifications());
        }
        //public Task<List<NotificationTemplateVO>> GetAllNotificationsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetAllNotificationsAsync());
        //}
    }
}