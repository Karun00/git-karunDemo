using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{

    public class RevenuePostingDtlVO
    {

        public int RevenuePostingDtlID { get; set; }
        public Nullable<int> RevenuePostingID { get; set; }
        public string GroupCode { get; set; }
        public string MaterialCode { get; set; }
        public string Units { get; set; }
        public string UOM { get; set; }
        public Nullable<int> ReferenceID { get; set; }
        public string MomentType { get; set; }
        public string ServiceType { get; set; }

        public Nullable<DateTime> PostedOn { get; set; }
        public virtual RevenuePostingVO RevenuePosting { get; set; }

    }
}
