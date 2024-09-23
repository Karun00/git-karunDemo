using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
   public class ServiceRequestVO
    {
       public ICollection<BerthVO> getBerths { get; set; }
        public ICollection<SubCategory> getMomentTypes { get; set; }
        public ICollection<SubCategory> getSideAlongSides { get; set; }
        public ICollection<SubCategory> getWarpSides { get; set; }
        public ICollection<SubCategory> getDocumenttypes { get; set; }
        public ICollection<UserMasterVO> UserDetails { get; set; }
        public ICollection<SlotVO> Slots { get; set; }
    }
}
