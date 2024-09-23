using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{

    public class RevenuePostingSectionsVO
    {

        public string VCN { get; set; }
        public int AgentID { get; set; }
        public string RegisteredName { get; set; }
        public int AgentAccountID { get; set; }
        public string AccountNo { get; set; }
        public List<MarineRevenuePostingVO> PortDuesDetails { get; set; } 
        public List<MarineRevenuePostingVO> BerthDuesDetails { get; set; }
        public List<MarineRevenuePostingVO> RefuseRemovalDetails { get; set; } 
        public List<MarineRevenuePostingVO> PortDuesDetailsView { get; set; }    
        public List<MarineRevenuePostingVO> ArrivalDetails { get; set; }
        public List<MarineRevenuePostingVO> ShiftingDetails { get; set; }
        public List<MarineRevenuePostingVO> WarpingDetails { get; set; }
        public List<MarineRevenuePostingVO> SailingDetails { get; set; }
        public List<MarineRevenuePostingVO> DryDockDetails { get; set; }
        public List<MarineRevenuePostingVO> DryDock12HrsDetails { get; set; }
        public List<MarineRevenuePostingVO> SupplimantoryDetails { get; set; }
        public List<MarineRevenuePostingVO> DrydockMislaniousDetails { get; set; }

        public List<MarineRevenuePostingVO> DisplayInfo { get; set; }

        
    }
}
