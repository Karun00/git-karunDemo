using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class ArrivalAgentMapExtension
    {
        public static List<ArrivalAgentVO> MapToDto(this IEnumerable<ArrivalAgent> arrivalAgent)
        {
            var ArrivalAgentList = new List<ArrivalAgentVO>();
            if (arrivalAgent != null)
            {
                foreach (var item in arrivalAgent)
                {
                    ArrivalAgentList.Add(item.MapToDto());
                }
            }
            return ArrivalAgentList;
        }

        public static List<ArrivalAgent> MapToEntity(this  IEnumerable<ArrivalAgentVO> arrivalAgentVO)
        {
            List<ArrivalAgent> ArrivalAgents = new List<ArrivalAgent>();

            if (arrivalAgentVO != null)
            {
                foreach (ArrivalAgentVO ArrivalAgent in arrivalAgentVO)
                {
                    ArrivalAgents.Add(ArrivalAgent.MapToEntity());
                }
            }
            return ArrivalAgents;
        }
        public static ArrivalAgentVO MapToDto(this ArrivalAgent data)
        {
            ArrivalAgentVO VO = new ArrivalAgentVO();
            if (data != null)
            {
                VO.ArrivalAgentID = data.ArrivalAgentID;
                VO.VCN = data.VCN;
                VO.AgentID = data.AgentID;
                VO.IsPrimary = data.IsPrimary;
            }
            return VO;
        }
        public static ArrivalAgent MapToEntity(this ArrivalAgentVO VO)
        {
            ArrivalAgent data = new ArrivalAgent();
            if (VO != null)
            {
                data.ArrivalAgentID = VO.ArrivalAgentID;
                data.VCN = VO.VCN;
                data.AgentID = VO.AgentID;
                data.IsPrimary = VO.IsPrimary;
            }
            return data;
        }
    }
}
