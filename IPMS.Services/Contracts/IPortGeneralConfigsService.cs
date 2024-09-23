using System;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IPortGeneralConfigsService
    {

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PortGeneralConfigsVO> GetAllPortGeneralConfigsDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PortGeneralConfigsVO> GetAllGroupNames(string GroupName);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int UpdatePortGeneralConfigs(PortGeneralConfigsVO portgeneralconfigdata);
    }
}
