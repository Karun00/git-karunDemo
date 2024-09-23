using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IMarpolService : IDisposable
    {
        [OperationContract]
        List<MarpolVO> GetMarpolDetails();

        [OperationContract]
        MarpolVO GetMarpolReferenceData();

        [OperationContract]
        MarpolVO SaveMarpolDetails(MarpolVO data);

        [OperationContract]
        MarpolVO ModifyMarpolDetails(MarpolVO data);
    }
}