using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IElectronicNotificationsService : IDisposable
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string AddNotification(NotificationTemplate notificationdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<Entity> GetEntityDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<string> GetTokens(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<Role> GetRolesDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<Port> GetPortsDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<NotificationDetails> GetNotifications();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<NotificationTemplateVO> GetAllNotifications();
        
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string DeleteNotification(NotificationTemplate notificationdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string ModifyNotification(NotificationTemplate notificationdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetWorkflowStatus();
    }
}
