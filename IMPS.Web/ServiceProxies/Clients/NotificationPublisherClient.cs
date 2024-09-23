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
    public class NotificationPublisherClient : UserClientBase<INotificationPublisherService>, INotificationPublisherService
    {
        public bool PushMessageToQueue(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode)
        {
            return WrapOperationWithException(() => Channel.PushMessageToQueue(entityId, reference, userid, company, portcode, workFlowTaskCode));
        }

        
    }
}