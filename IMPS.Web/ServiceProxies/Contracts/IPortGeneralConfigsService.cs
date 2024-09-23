using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IPortGeneralConfigsService : IDisposable
    {
        [OperationContract]
        List<PortGeneralConfigsVO> GetAllPortGeneralConfigsDetails();

        [OperationContract]
        List<PortGeneralConfigsVO> GetAllGroupNames(string GroupName);

        [OperationContract]
        int UpdatePortGeneralConfigs(PortGeneralConfigsVO portgeneralconfigdata);
    }
}
