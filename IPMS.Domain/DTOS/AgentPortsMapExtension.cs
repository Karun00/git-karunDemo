using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class AgentPortsMapExtension
    {
        public static List<AgentPortsVO> MapToListDTO(this IEnumerable<AgentPort> agentPortsList)
        {
            List<AgentPortsVO> agentPortsVoList = new List<AgentPortsVO>();
            if (agentPortsList != null)
            {
                foreach (var agentPort in agentPortsList)
                {
                    AgentPortsVO agentPortVo = new AgentPortsVO();
                    agentPortVo.AgentPortID = agentPort.AgentPortID;
                    agentPortVo.WorkflowInstanceId = agentPort.WorkflowInstanceId;
                    agentPortVo.AgentID = agentPort.AgentID;
                    agentPortVo.PortCode = agentPort.PortCode;
                    agentPortVo.WFStatus = agentPort.WFStatus;
                    agentPortVo.VerifiedBy = agentPort.VerifiedBy;
                    agentPortVo.VerifiedDate = agentPort.VerifiedDate;
                    agentPortVo.ApprovedBy = agentPort.ApprovedBy;
                    agentPortVo.ApprovedDate = agentPort.ApprovedDate;
                    agentPortVo.RejectComments = agentPort.RejectComments;
                    agentPortVo.RecordStatus = agentPort.RecordStatus;
                    agentPortVo.CreatedBy = agentPort.CreatedBy;
                    agentPortVo.CreatedDate = agentPort.CreatedDate;
                    agentPortVo.ModifiedBy = agentPort.ModifiedBy;
                    agentPortVo.ModifiedDate = agentPort.ModifiedDate;
                    agentPortsVoList.Add(agentPortVo);
                }
            }
            return agentPortsVoList;
        }
        public static List<AgentPort> MapToListEntity(this IEnumerable<AgentPortsVO> agentPortsVoList)
        {
            List<AgentPort> agentPortsList = new List<AgentPort>();
            if (agentPortsVoList != null)
            {
                foreach (var agentPortVo in agentPortsVoList)
                {
                    AgentPort agentPort = new AgentPort();
                    agentPort.AgentPortID = agentPortVo.AgentPortID;
                    agentPort.WorkflowInstanceId = agentPortVo.WorkflowInstanceId;
                    agentPort.AgentID = agentPortVo.AgentID;
                    agentPort.PortCode = agentPortVo.PortCode;
                    agentPort.WFStatus = agentPortVo.WFStatus;
                    agentPort.VerifiedBy = agentPortVo.VerifiedBy;
                    agentPort.VerifiedDate = agentPortVo.VerifiedDate;
                    agentPort.ApprovedBy = agentPortVo.ApprovedBy;
                    agentPort.ApprovedDate = agentPortVo.ApprovedDate;
                    agentPort.RejectComments = agentPortVo.RejectComments;
                    agentPort.RecordStatus = agentPortVo.RecordStatus;
                    agentPort.CreatedBy = agentPortVo.CreatedBy;
                    agentPort.CreatedDate = agentPortVo.CreatedDate;
                    agentPort.ModifiedBy = agentPortVo.ModifiedBy;
                    agentPort.ModifiedDate = agentPortVo.ModifiedDate;
                    agentPortsList.Add(agentPort);
                }
            }
            return agentPortsList;
        }
        public static AgentPortsVO MapToDTO(this AgentPort data)
        {
            AgentPortsVO agentPortsVo = new AgentPortsVO();
            if (data != null)
            {
                agentPortsVo.AgentPortID = data.AgentPortID;
                agentPortsVo.AgentID = data.AgentID;
                agentPortsVo.PortCode = data.PortCode;
                agentPortsVo.WFStatus = data.WFStatus;
                agentPortsVo.VerifiedBy = data.VerifiedBy;
                agentPortsVo.VerifiedDate = data.VerifiedDate;
                agentPortsVo.ApprovedBy = data.ApprovedBy;
                agentPortsVo.ApprovedDate = data.ApprovedDate;
                agentPortsVo.RejectComments = data.RejectComments;
                agentPortsVo.RecordStatus = data.RecordStatus;
                agentPortsVo.CreatedBy = data.CreatedBy;
                agentPortsVo.CreatedDate = data.CreatedDate;
                agentPortsVo.ModifiedBy = data.ModifiedBy;
                agentPortsVo.ModifiedDate = data.ModifiedDate;
            }
            return agentPortsVo;
        }
        public static AgentPort MapToEntity(this AgentPortsVO vo)
        {
            AgentPort agentPort = new AgentPort();
            if (vo != null)
            {
                agentPort.AgentPortID = vo.AgentPortID;
                agentPort.AgentID = vo.AgentID;
                agentPort.PortCode = vo.PortCode;
                agentPort.WFStatus = vo.WFStatus;
                agentPort.VerifiedBy = vo.VerifiedBy;
                agentPort.VerifiedDate = vo.VerifiedDate;
                agentPort.ApprovedBy = vo.ApprovedBy;
                agentPort.ApprovedDate = vo.ApprovedDate;
                agentPort.RejectComments = vo.RejectComments;
                agentPort.RecordStatus = vo.RecordStatus;
                agentPort.CreatedBy = vo.CreatedBy;
                agentPort.CreatedDate = vo.CreatedDate;
                agentPort.ModifiedBy = vo.ModifiedBy;
                agentPort.ModifiedDate = vo.ModifiedDate;
            }
            return agentPort;
        }
    }
}
