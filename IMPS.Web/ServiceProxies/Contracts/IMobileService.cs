using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using System;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IMobileService : IDisposable
    {
        [OperationContract]
        IEnumerable<MobileModuleVO> GetModulesForMobile();

        [OperationContract]
        IEnumerable<SystemNotificationVO> GetNotifications();

        [OperationContract]
        IEnumerable<EntityVO> GetFeatures();

        [OperationContract]
        int ModifyNotifications(SystemNotification notificationData);

        [OperationContract]
        int ModifyNotificationsByID(string notificationID);

        [OperationContract]
        IEnumerable<SystemNotificationVO> GetNewNotifications();

        [OperationContract]
        IEnumerable<PlannedMovementsVO> GetPlannedMovements();

        [OperationContract]
        IEnumerable<PlannedMovementsVO> GetPlannedMovementsForDesktop(string PortCode);

        [OperationContract]
        IEnumerable<PlannedMovementsVO> GetPlannedMovementsForAnonymous(string portCode);

        [OperationContract]
        List<ArrvNotfMobileAppVo> GetVCNStatusForMobApp(string VCN);
    }
}

