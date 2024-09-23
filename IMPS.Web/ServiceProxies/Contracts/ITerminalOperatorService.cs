using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ITerminalOperatorService : IDisposable
    {
        [OperationContract]
        List<TerminalOperatorVO> GetTerminalOperatorList();

        //[OperationContract]
        //Task<List<TerminalOperatorVO>> GetTerminalOperatorListAsync();

        [OperationContract]
        TerminalOperatorVO AddTerminalOperator(TerminalOperatorVO terminalOperatorData);

        //[OperationContract]
        //Task<TerminalOperatorVO> AddterminalOperatorAsync(TerminalOperatorVO terminaloperatordata);
        
        [OperationContract]
        TerminalOperatorVO ModifyTerminalOperator(TerminalOperatorVO terminalOperatorData);

        //[OperationContract]
        //Task<TerminalOperatorVO> ModifyterminalOperatorAsync(TerminalOperatorVO terminaloperatordata);

        [OperationContract]
        List<SubCategoryVO> GetCargoTypes();
    }
}
