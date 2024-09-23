using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ITerminalOperatorService 
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<TerminalOperatorVO> GetTerminalOperatorList();
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetCargoTypes();
        [OperationContract]
        [FaultContract(typeof(Exception))]
        TerminalOperatorVO AddTerminalOperator(TerminalOperatorVO terminalOperatorData);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        TerminalOperatorVO ModifyTerminalOperator(TerminalOperatorVO terminalOperatorData);

    }
}
