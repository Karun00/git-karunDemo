using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface INotificationPublisherService
    {
        [OperationContract]
        bool PushMessageToQueue(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode);
    }
}
