using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Clients
{
    public class RevenueStopListService : UserClientBase<IRevenueStopListService>, IRevenueStopListService
    {

        public RevenueStopReferenceDataVO GetRevennueStopReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetRevennueStopReferenceVO());
        }
        public List<RevenueStopListVO> SearchRevennueStop(string AgentID, string agentname, string horbaraccountno, string accountstatus)
        { 
            return WrapOperationWithException(() => Channel.SearchRevennueStop(AgentID,agentname,horbaraccountno,accountstatus)); 
        }
        public List<RevenueStopListVO> GetAgentdetails(string ag)
        {
            return WrapOperationWithException(() => Channel.GetAgentdetails(ag)); 
        }

        public List<RevenueStopListVO> GetAllAgentsforgrid()
        {
            return WrapOperationWithException(() => Channel.GetAllAgentsforgrid());
        }

        public RevenueStopListVO ModifyRevenueStop(RevenueStopListVO revenueStop)
        {
            return WrapOperationWithException(() => Channel.ModifyRevenueStop(revenueStop)); 
        }
        public RevenueStopListVO AddRevenueStop(RevenueStopListVO revenueStop)
        {
            return WrapOperationWithException(() => Channel.AddRevenueStop(revenueStop)); 
        }


    }
}