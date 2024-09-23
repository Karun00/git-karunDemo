//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    public partial class SlotPriorityConfigurationVO
    {
        public int SlotCofiguratinid { get; set; }
        public string VesselType { get; set; }
        public int Priority { get; set; }
        public int NoofVessels { get; set; }
        public string RecordStatus { get; set; }
        public string VesselTypeName { get; set; } 
      //  public SubCategoryVO SubCategory { get; set; }
    }
}
