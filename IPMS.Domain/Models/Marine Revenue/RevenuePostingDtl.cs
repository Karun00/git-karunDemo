using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
    public partial class RevenuePostingDtl : EntityBase
    {
        public int RevenuePostingDtlID { get; set; }

        public int RevenuePostingDtlSrno { get; set; }

        public string VCN { get; set; }
        public Nullable<int> RevenuePostingID { get; set; }
        public string GroupCode { get; set; }
        public string MaterialCode { get; set; }
        public string Units { get; set; }
        public string UOM { get; set; }
        public Nullable<int> ReferenceID { get; set; }
        public string MomentType { get; set; }
        public string ServiceType { get; set; }
        public virtual RevenuePosting RevenuePosting { get; set; }
        public Nullable<System.DateTime> PostedOn { get; set; }
        public Nullable<System.DateTime> PostingFrom { get; set; }
    }
}
