using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IRevenueStopListService : IDisposable
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        RevenueStopReferenceDataVO GetRevennueStopReferenceVO();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenueStopListVO> GetAgentdetails(string ag);
       
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenueStopListVO> GetAllAgentsforgrid();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenueStopListVO> SearchRevennueStop(string AgentID, string agentname, string horbaraccountno, string accountstatus);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        RevenueStopListVO AddRevenueStop(RevenueStopListVO revenueStop);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        RevenueStopListVO ModifyRevenueStop(RevenueStopListVO revenueStop);

    }
}

