using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
     public interface IRevenueStopListRepository
    {
        //List<AgentVO> GetAllAgentaccounts(string portcode);
        //List<AgentVO> GetAllAcountstatus(string portcode);
        //List<AgentVO> GetAllAgentAccounts(string portcode);

         List<RevenueStopListVO> GetAllAgentsforgrid(string portcode);
         List<RevenueStopListVO> GetAllAgents(string portcode, string ag);
         List<RevenueStopListVO> GetSearchAgentData(string AgentID, string agentname, string horbaraccountno, string accountstatus,string portcode);
          
    }
}

