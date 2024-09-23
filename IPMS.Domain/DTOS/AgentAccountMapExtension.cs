using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class AgentAccountMapExtension
    {
        public static List<AgentAccountVO> MapToDTO(this IEnumerable<AgentAccount> agentAccount)
        {
            var agentAccountList = new List<AgentAccountVO>();
            if (agentAccount != null)
            {
                foreach (var item in agentAccount)
                {
                    agentAccountList.Add(item.MapToDTO());
                }
            }
            return agentAccountList;
        }

        public static List<AgentAccount> MapToEntity(this List<AgentAccountVO> agentAccountsvo)
        {
            List<AgentAccount> agentAccounts = new List<AgentAccount>();
            if (agentAccountsvo != null)
            {
                foreach (AgentAccountVO agentAccount in agentAccountsvo)
                {
                    agentAccounts.Add(agentAccount.MapToEntity());
                }
            }

            return agentAccounts;
        }
        public static AgentAccountVO MapToDTO(this AgentAccount data)
        {
            AgentAccountVO VO = new AgentAccountVO();
            if (data != null)
            {
                VO.PortCode = data.PortCode;
                //VO.AccountName = data.AccountName;
                VO.AccountNo = data.AccountNo;
                VO.AgentAccountID = data.AgentAccountID;
                VO.AgentID = data.AgentID;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
                VO.RecordStatus = data.RecordStatus;
            }
            return VO;
        }
        public static AgentAccount MapToEntity(this AgentAccountVO VO)
        {
            AgentAccount data = new AgentAccount();
            if (VO != null)
            {
                data.PortCode = VO.PortCode;
                //data.AccountName = VO.AccountName;
                data.AccountNo = VO.AccountNo.Trim();
                data.AgentAccountID = VO.AgentAccountID;
                data.AgentID = VO.AgentID;
                data.CreatedBy = VO.CreatedBy;
                data.CreatedDate = VO.CreatedDate;
                data.ModifiedBy = VO.ModifiedBy;
                data.ModifiedDate = VO.ModifiedDate;
                data.RecordStatus = VO.RecordStatus;
            }
            return data;
        }
    }
}
