using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceAllocationReferenceDataVO
    {
        public ICollection<BerthVO> Berths { get; set; }
        public ICollection<BollardVO> Bollards { get; set; }
        public ICollection<SubCategoryCodeNameVO> MopsDelays { get; set; }
        // Delay Reasons code
        public ICollection<SubCategoryCodeNameVO> DelayReasons { get; set; }
        public ICollection<UserMasterVO> FloatingResources { get; set; }
        public ICollection<UserMasterVO> WaterResources { get; set; }
        
    }
}
