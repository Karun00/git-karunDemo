using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{

    public static class AgentMapExtension
    {
        public static List<AgentVO> MapToDTO(this List<Agent> Agents)
        {
            List<AgentVO> AgentVos = new List<AgentVO>();
            foreach (var Agent in Agents)
            {
                AgentVos.Add(Agent.MapToDTO());
            }
            return AgentVos;
        }

        public static List<AgentVO> MapToDToIDNAME(this List<Agent> Agents)
        {
            List<AgentVO> AgentVos = new List<AgentVO>();
            if (Agents != null)
            {
                foreach (var Agent in Agents)
                {
                    AgentVos.Add(Agent.MapToDToIDNAME());
                }
            }
            return AgentVos;
        }

        public static AgentVO MapToDToIDNAME(this Agent data)
        {
            AgentVO agentVo = new AgentVO();
            if (data != null)
            {
                agentVo.AgentID = data.AgentID;
                agentVo.RegisteredName = data.RegisteredName;
            }
            return agentVo;
        }

        public static AgentVO MapToDTO(this Agent data)
        {
            AgentVO agentVo = new AgentVO();
            if (data != null)
            {
                agentVo.AgentID = data.AgentID;
                agentVo.AnonymousUserYn = data.AnonymousUserYn;
                agentVo.ReferenceNo = data.ReferenceNo;
                agentVo.WorkflowInstanceId = data.WorkflowInstanceId;
                agentVo.RegisteredName = data.RegisteredName;
                agentVo.TradingName = data.TradingName;
                agentVo.RegistrationNumber = data.RegistrationNumber;
                agentVo.VATNumber = data.VATNumber;
                agentVo.IncomeTaxNumber = data.IncomeTaxNumber;
                agentVo.SkillsDevLevyNumber = data.SkillsDevLevyNumber;
                agentVo.BusinessAddressID = data.BusinessAddressID;
                agentVo.PostalAddressID = data.PostalAddressID;
                agentVo.TelephoneNo1 = data.TelephoneNo1;
                agentVo.TelephoneNo2 = data.TelephoneNo2;
                agentVo.FaxNo = data.FaxNo;
                agentVo.AuthorizedContactPersonID = data.AuthorizedContactPersonID;
                agentVo.SARSTaxClearance = data.SARSTaxClearance;
                agentVo.SAASOA = data.SAASOA;
                agentVo.QualifyBBBEECodes = data.QualifyBBBEECodes;
                agentVo.BBBEEStatus = data.BBBEEStatus;
                agentVo.VerifyBBBEEStatus = data.VerifyBBBEEStatus;
                agentVo.RecordStatus = data.RecordStatus;
                //agentVo.FromDate = data.FromDate;
                //agentVo.ToDate = data.ToDate;

                agentVo.CreatedBy = data.CreatedBy;
                agentVo.CreatedDate = data.CreatedDate;
                agentVo.ModifiedBy = data.ModifiedBy;
                agentVo.ModifiedDate = data.ModifiedDate;
            }
            return agentVo;
        }
        public static Agent MapToEntity(this AgentVO vo)
        {
            Agent agent = new Agent();
            if (vo != null)
            {
                agent.AgentID = vo.AgentID;
                agent.AnonymousUserYn = vo.AnonymousUserYn;
                agent.ReferenceNo = vo.ReferenceNo;
                agent.WorkflowInstanceId = vo.WorkflowInstanceId;
                agent.RegisteredName = vo.RegisteredName;
                agent.TradingName = vo.TradingName;
                agent.RegistrationNumber = vo.RegistrationNumber;
                agent.VATNumber = vo.VATNumber;
                agent.IncomeTaxNumber = vo.IncomeTaxNumber;
                agent.SkillsDevLevyNumber = vo.SkillsDevLevyNumber;
                agent.BusinessAddressID = vo.BusinessAddressID;
                agent.PostalAddressID = vo.PostalAddressID;
                agent.TelephoneNo1 = vo.TelephoneNo1;
                agent.TelephoneNo2 = vo.TelephoneNo2;
                agent.FaxNo = vo.FaxNo;
                agent.AuthorizedContactPersonID = vo.AuthorizedContactPersonID;
                agent.SARSTaxClearance = vo.SARSTaxClearance;
                agent.SAASOA = vo.SAASOA;
                agent.QualifyBBBEECodes = vo.QualifyBBBEECodes;
                agent.BBBEEStatus = vo.BBBEEStatus;
                agent.VerifyBBBEEStatus = vo.VerifyBBBEEStatus;
                agent.RecordStatus = vo.RecordStatus;
                //agent.FromDate = vo.FromDate;
                //agent.ToDate = vo.ToDate;

                agent.CreatedBy = vo.CreatedBy;
                agent.CreatedDate = vo.CreatedDate;
                agent.ModifiedBy = vo.ModifiedBy;
                agent.ModifiedDate = vo.ModifiedDate;
                agent.SubmissionDate = vo.CreatedDate;
            }
            return agent;
        }
    }
}
