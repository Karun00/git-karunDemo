using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class RevenueStopReferenceDataVO
    {
        public List<AgentVO> AgentDetails { get; set; }
        public ICollection<SubCategoryCodeNameVO> RevenueAccountStatus { get; set; }
        public ICollection<RevenueStopListVO> Agentname { get; set; }
        public ICollection<RevenueStopListVO> Agentcode{ get; set; }
        public ICollection<RevenueStopListVO> HarborAccountno { get; set; }
    }
}
