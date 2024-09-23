using System.Collections.Generic;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Clients
{
    public class MobileClient : UserClientBase<IMobileService>, IMobileService
    {
        public IEnumerable<MobileModuleVO> GetModulesForMobile()
        {
            return WrapOperationWithException(() => Channel.GetModulesForMobile());
        }

        public IEnumerable<SystemNotificationVO> GetNotifications()
        {
            return WrapOperationWithException(() => Channel.GetNotifications());
        }

        public IEnumerable<EntityVO> GetFeatures()
        {
            return WrapOperationWithException(() => Channel.GetFeatures());
        }

        public int ModifyNotifications(SystemNotification notificationData)
        {
            return WrapOperationWithException(() => Channel.ModifyNotifications(notificationData));
        }

        public int ModifyNotificationsByID(string notificationID)
        {
            return WrapOperationWithException(() => Channel.ModifyNotificationsByID(notificationID));
        }

        public IEnumerable<SystemNotificationVO> GetNewNotifications()
        {
            return WrapOperationWithException(() => Channel.GetNewNotifications());
        }

        public IEnumerable<PlannedMovementsVO> GetPlannedMovements()
        {
            return WrapOperationWithException(() => Channel.GetPlannedMovements());
        }

        public IEnumerable<PlannedMovementsVO> GetPlannedMovementsForDesktop(string PortCode)
        {
            return WrapOperationWithException(() => Channel.GetPlannedMovementsForDesktop(PortCode));
        }

        public IEnumerable<PlannedMovementsVO> GetPlannedMovementsForAnonymous(string portCode)
        {
            return WrapOperationWithException(() => Channel.GetPlannedMovementsForAnonymous(portCode));
        }

        public List<ArrvNotfMobileAppVo> GetVCNStatusForMobApp(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetVCNStatusForMobApp(VCN));
        }

    }
}
