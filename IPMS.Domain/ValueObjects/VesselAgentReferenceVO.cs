using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    public class VesselAgentReferenceVO
    {
        public ICollection<ArrivalApprovalVO> getVCN { get; set; }
        public ICollection<SubCategoryVO> getResonForTransfer { get; set; }
        public ICollection<AgentVO> getPraposedAgents { get; set; }
        public ICollection<VesselVO> getVesselDetails { get; set; }
        public ICollection<SubCategoryVO> getDocumentTypes { get; set; }
    }
}
