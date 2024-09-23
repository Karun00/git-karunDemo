using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IMobileService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<MobileModuleVO> GetModulesForMobile();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<SystemNotificationVO> GetNotifications();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<EntityVO> GetFeatures();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int ModifyNotifications(SystemNotification notificationData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int ModifyNotificationsByID(string notificationID);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<SystemNotificationVO> GetNewNotifications();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<PlannedMovementsVO> GetPlannedMovements();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<PlannedMovementsVO> GetPlannedMovementsForDesktop(string PortCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<PlannedMovementsVO> GetPlannedMovementsForAnonymous(string portCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ArrvNotfMobileAppVo> GetVCNStatusForMobApp(string VCN);
    }
}

