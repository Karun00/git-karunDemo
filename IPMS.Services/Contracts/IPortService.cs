using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IPortService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        Port AddPort(Port portData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Port ModifyPort(Port portData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Port GetPortId(string code);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Port DelPort(long id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<Port> GetPorts();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<Port> GetLoginPort();
    }
}