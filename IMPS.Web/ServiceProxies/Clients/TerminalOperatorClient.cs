using System;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;


namespace IPMS.Web.ServiceProxies.Clients
{
    public class TerminalOperatorClient : UserClientBase<ITerminalOperatorService>, ITerminalOperatorService
    {
        public List<TerminalOperatorVO> GetTerminalOperatorList()
        {
            return WrapOperationWithException(() => Channel.GetTerminalOperatorList());
        }

        //public Task<List<TerminalOperatorVO>> GetTerminalOperatorListAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetTerminalOperatorListAsync());
        //}

        public List<SubCategoryVO> GetCargoTypes()
        {
            return WrapOperationWithException(() => Channel.GetCargoTypes());
        }

        public TerminalOperatorVO AddTerminalOperator(TerminalOperatorVO terminalOperatorData)
        {
            return WrapOperationWithException(() => Channel.AddTerminalOperator(terminalOperatorData));
        }

        //public Task<TerminalOperatorVO> AddterminalOperatorAsync(TerminalOperatorVO terminaloperatordata)
        //{
        //    return WrapOperationWithException(() => Channel.AddterminalOperatorAsync(terminaloperatordata));
        //}

        public TerminalOperatorVO ModifyTerminalOperator(TerminalOperatorVO terminalOperatorData)
        {
            return WrapOperationWithException(() => Channel.ModifyTerminalOperator(terminalOperatorData));
        }

        //public Task<TerminalOperatorVO> ModifyterminalOperatorAsync(TerminalOperatorVO terminaloperatordata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyterminalOperatorAsync(terminaloperatordata));
        //}
    }
}