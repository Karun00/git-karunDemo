using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ISuppDockUnDockTimeService : IDisposable
    {
        [OperationContract]
        List<SuppDryDockVO> AllSuppDockUnDockTimeDetails();

        [OperationContract]
        SuppDryDockVO ModifySuppDockUnDockTime(SuppDryDockVO data);

    }
}
