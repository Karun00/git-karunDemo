    using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
   [ServiceContract]
    public interface IRevenueStopListService: IDisposable
    {
       [OperationContract]
       RevenueStopReferenceDataVO GetRevennueStopReferenceVO();
       [OperationContract]
       List<RevenueStopListVO> SearchRevennueStop(string AgentID, string agentname, string horbaraccountno, string accountstatus);
       [OperationContract]
       List<RevenueStopListVO> GetAgentdetails(string ag);
       [OperationContract]
       RevenueStopListVO ModifyRevenueStop(RevenueStopListVO revenueStop);
       [OperationContract]
       RevenueStopListVO AddRevenueStop(RevenueStopListVO revenueStop);
       [OperationContract]
       List<RevenueStopListVO> GetAllAgentsforgrid();
    }
}

