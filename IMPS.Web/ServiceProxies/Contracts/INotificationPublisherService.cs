using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;


namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface INotificationPublisherService : IDisposable
    {
        [OperationContract]
        bool PushMessageToQueue(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode);

    }
}
